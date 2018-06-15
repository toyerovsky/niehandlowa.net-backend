using Microsoft.EntityFrameworkCore;
using niehandlowa.net.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace niehandlowa.net.Dal.Repository
{
    public abstract class BaseRepository<TObject> : IBaseRepository<TObject> where TObject : class, IEntity
    {
        protected NonTradeContext _context;

        public BaseRepository(NonTradeContext context)
        {
            _context = context;
        }

        private static IQueryable<TObject> PrepareResult(IQueryable<TObject> result, Func<IQueryable<TObject>, IOrderedQueryable<TObject>> orderBy = null,
                                                 Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null, int? skip = null, int? take = null)
        {
            if (includeMembers != null)
            {
                result = includeMembers(result);
            }
            if (orderBy != null)
            {
                result = orderBy(result);
            }
            if (skip != null)
            {
                result = result.Skip(skip.Value);
            }
            if (take != null)
            {
                result = result.Take(take.Value);
            }

            return result;
        }

        public virtual IEnumerable<TObject> GetAll(Func<IQueryable<TObject>, IOrderedQueryable<TObject>> orderBy = null,
                                                 Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null, int? skip = null, int? take = null)
        {
            var result = _context.Set<TObject>().AsQueryable();
            return PrepareResult(result, orderBy, includeMembers, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TObject>> GetAllAsync(Func<IQueryable<TObject>, IOrderedQueryable<TObject>> orderBy = null,
                                                 Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null, int? skip = null, int? take = null)
        {
            var result = _context.Set<TObject>().AsQueryable();
            return await PrepareResult(result, orderBy, includeMembers, skip, take).ToListAsync();
        }

        public virtual TObject Find(int id, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return result.First(r => r.Id == id);
        }

        public virtual TObject FindDefault(int id, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return result.FirstOrDefault(r => r.Id == id);
        }

        public virtual async Task<TObject> FindAsync(int id, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return await result.FirstAsync(r => r.Id == id);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return result.First(match);
        }

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return await result.FirstAsync(match);
        }

        public virtual IEnumerable<TObject> FindAll(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IOrderedQueryable<TObject>> orderBy = null,
                                                         Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null, int? skip = null, int? take = null)
        {
            var result = _context.Set<TObject>().Where(match).AsQueryable();

            result = PrepareResult(result, orderBy, includeMembers, skip, take);

            return result.ToList();
        }

        public virtual IQueryable<TObject> AsQueryable()
        {
            return _context.Set<TObject>().AsQueryable();
        }

        public virtual async Task<IEnumerable<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IOrderedQueryable<TObject>> orderBy = null,
                                                         Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null, int? skip = null, int? take = null)
        {
            var result = _context.Set<TObject>().Where(match).AsQueryable();

            result = PrepareResult(result, orderBy, includeMembers, skip, take);

            return await result.ToListAsync();
        }

        //public virtual async Task<IEnumerable<TObject>> FindAllAsyncByJoin(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IQueryable<TObject>> joinMembers = null)
        //{
        //    var querry = _context.Set<TObject>().Join(_context.VendorEntity, c => c.Id, d => d.Id, (c, d) => new { Category = c, CategoryMaps = d }).Select(x => x.Category).Where(match);

        //    var querry2 = _context.WeekDeliveryEntity.Join(_context.VendorEntity, c => c.VendorId, d => d.Id, (c, d) => new VendorEntity { WeekDeliveryEntity = c }).Select(x => x.Category).Where(match);

        //    //         var query = db.Categories         // source
        //    //.Join(db.CategoryMaps,         // target
        //    //   c => c.CategoryId,          // FK
        //    //   cm => cm.ChildCategoryId,   // PK
        //    //   (c, cm) => new { Category = c, CategoryMaps = cm }) // project result
        //    //.Select(x => x.Category);  // select result

        //    return null;
        //}

        public virtual TObject Add(TObject t)
        {
            _context.Set<TObject>().Add(t);
            return t;
        }

        public virtual async Task<TObject> AddAsync(TObject t)
        {
            await _context.Set<TObject>().AddAsync(t);
            return t;
        }

        public virtual IEnumerable<TObject> AddRange(IEnumerable<TObject> t)
        {
            _context.Set<TObject>().AddRange(t);
            return t;
        }

        public async Task<IEnumerable<TObject>> AddRangeAsync(IEnumerable<TObject> t)
        {
            await _context.Set<TObject>().AddRangeAsync(t);
            _context.SaveChanges();
            return t;
        }

        public virtual TObject Update(TObject t)
        {
            _context.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public virtual async Task<TObject> UpdateAsync(TObject t)
        {
            _context.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }

        public virtual IEnumerable<TObject> UpdateRange(IEnumerable<TObject> t)
        {
            _context.Set<TObject>().UpdateRange(t);
            return t;
        }

        public virtual async Task<IEnumerable<TObject>> UpdateRangeAsync(IEnumerable<TObject> t)
        {
            _context.Set<TObject>().UpdateRange(t);

            return t;
        }

        public virtual void Delete(TObject t)
        {
            _context.Set<TObject>().Remove(t);
        }

        public virtual async Task DeleteAsync(TObject t)
        {
            _context.Set<TObject>().Remove(t);
        }

        public virtual void DeleteRange(IEnumerable<TObject> t)
        {
            _context.Set<TObject>().RemoveRange(t);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TObject> t)
        {
            _context.Set<TObject>().RemoveRange(t);
        }

        public virtual int Count()
        {
            return _context.Set<TObject>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<TObject>().CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().CountAsync(match);
        }

        public async Task<TObject> FindAsyncDefault(Expression<Func<TObject, bool>> match, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable().AsNoTracking();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return await result.FirstOrDefaultAsync(match);
        }

        public async Task<TObject> FindAsyncDefault(int id, Func<IQueryable<TObject>, IQueryable<TObject>> includeMembers = null)
        {
            var result = _context.Set<TObject>().AsQueryable();

            if (includeMembers != null)
            {
                result = includeMembers(result);
            }

            return await result.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
