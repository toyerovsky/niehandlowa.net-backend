using AutoMapper;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using niehandlowa.net.Bll.Helpers;
using niehandlowa.net.Bll.Models;
using niehandlowa.net.Dal.Entities;
using niehandlowa.net.Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace niehandlowa.net.Bll.Services
{
    public class POIService : IPOIService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public POIService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<POIModel>> GetAllPOIs()
        {
            return _mapper.Map<List<POIModel>>(await _unitOfWork.POIRepository.GetAllAsync(null, i => i.Include(p => p.OpeningHours))).ToList();
        }

        public async Task<List<POIModel>> GetPOIsByType(int type)
        {
            return _mapper.Map<List<POIModel>>((await _unitOfWork.POIRepository.FindAllAsync(p => p.Type == type, null, i => i.Include(p => p.OpeningHours))).ToList());
        }

        public async Task<List<POIModel>> GetPOIsWithinDistance(double latitude, double longitude, int distance)
        {
            var userCoordinates = new GeoCoordinate(latitude, longitude);
            var pois = await GetAllPOIs();
            List<POIModel> resultList = new List<POIModel>();
            foreach (var poi in pois)
            {
                var poiCoordinates = new GeoCoordinate(poi.Latitude, poi.Longitude);

                if (poiCoordinates.GetDistanceTo(userCoordinates) <= distance)
                {
                    resultList.Add(poi);
                }
            }
            return resultList;
        }

        public async Task<List<POIModel>> GetPOIsWithinDistanceByTypesList(double latitude, double longitude, int distance, List<int> types)
        {
            var userCoordinates = new GeoCoordinate(latitude, longitude);
            var poisByType = _mapper.Map<List<POIModel>>(await _unitOfWork.POIRepository.FindAllAsync(p => types.Contains(p.Type), null, i => i.Include(p => p.OpeningHours))).ToList();
            List<POIModel> resultList = new List<POIModel>();
            foreach (var poi in poisByType)
            {
                var poiCoordinates = new GeoCoordinate(poi.Latitude, poi.Longitude);

                if (poiCoordinates.GetDistanceTo(userCoordinates) <= distance)
                {
                    resultList.Add(poi);
                }
            }
            return resultList;
        }

        public async Task Create(POIModel model)
        {
            model.Id = 0;
            foreach (var item in model.OpeningHours)
            {
                item.Id = 0;
            }
            await _unitOfWork.POIRepository.AddAsync(_mapper.Map<POIEntity>(model));
            await _unitOfWork.ComitAsync();
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await _unitOfWork.POIRepository.FindAsync(id);
            await _unitOfWork.POIRepository.DeleteAsync(entityToDelete);
            await _unitOfWork.ComitAsync();
        }

        public async Task Update(POIModel model)
        {
            await _unitOfWork.POIRepository.UpdateAsync(_mapper.Map<POIModel>(model));
            await _unitOfWork.ComitAsync();
        }

        public async Task<bool> IsPOIOpenAtTime(int POIId, DateTime date, bool? tradeSunday = null)
        {
            var day = date.DayOfWeek;
            int intDayOfWeek = 0;
            switch (day)
            {
                case DayOfWeek.Monday:
                    intDayOfWeek = 1;
                    break;
                case DayOfWeek.Tuesday:
                    intDayOfWeek = 2;
                    break;
                case DayOfWeek.Wednesday:
                    intDayOfWeek = 3;
                    break;
                case DayOfWeek.Thursday:
                    intDayOfWeek = 4;
                    break;
                case DayOfWeek.Friday:
                    intDayOfWeek = 5;
                    break;
                case DayOfWeek.Saturday:
                    intDayOfWeek = 6;
                    break;
                case DayOfWeek.Sunday:
                    if (tradeSunday.Value)
                        intDayOfWeek = 7;
                    else
                        intDayOfWeek = 8;
                    break;
            }

            var openingHours = await _unitOfWork.OpeningHoursRepository.FindAllAsync(o => o.POI.Id == POIId && o.DayOfWeek == intDayOfWeek);
            if (!openingHours.Any())
            {
                return false;
            }

            DateTime openingHourUniversalTime = new DateTime(2000, 1, 1, openingHours.First().OpeningTime.Hour, openingHours.First().OpeningTime.Minute, openingHours.First().OpeningTime.Second);
            DateTime closingHourUniversalTime = new DateTime(2000, 1, 1, openingHours.First().ClosingTime.Hour, openingHours.First().ClosingTime.Minute, openingHours.First().ClosingTime.Second);

            DateTime userGivenTimeUniversalime = new DateTime(2000, 1, 1, date.Hour, date.Minute, date.Second);

            if (userGivenTimeUniversalime >= openingHourUniversalTime && userGivenTimeUniversalime <= closingHourUniversalTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<POIModel>> GetPOIsByTypesList(List<int> types)
        {
            return _mapper.Map<List<POIModel>>(await _unitOfWork.POIRepository.FindAllAsync(p => types.Contains(p.Type), null, i => i.Include(p => p.OpeningHours))).ToList();
        }

        public async Task<List<POIModel>> GetNowOpenPOIs()
        {
            var allPois = await GetAllPOIs();
            List<POIModel> resultList = new List<POIModel>();

            DateTime nowTime = DateTime.Now;
            foreach (var poi in allPois)
            {
                if (nowTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (await IsPOIOpenAtTime(poi.Id, nowTime, DayHelper.IsSundayTrading(nowTime)))
                    {
                        resultList.Add(poi);
                    }
                }
                else
                {
                    if (await IsPOIOpenAtTime(poi.Id, nowTime))
                    {
                        resultList.Add(poi);
                    }
                }
            }
            return resultList;
        }

        public async Task GiveLikeForPOI(int id)
        {
            var POI = await _unitOfWork.POIRepository.FindAsync(id);
            POI.LikesCount++;
            await _unitOfWork.POIRepository.UpdateAsync(POI);
            await _unitOfWork.ComitAsync();
        }

        public async Task GiveDislikeForPOI(int id)
        {
            var POI = await _unitOfWork.POIRepository.FindAsync(id);
            POI.DislikesCount++;
            await _unitOfWork.POIRepository.UpdateAsync(POI);
            await _unitOfWork.ComitAsync();
        }

        public async Task<int> GetDifferenceLikesForPOI(int id)
        {
            var POI = await _unitOfWork.POIRepository.FindAsync(id);
            return (POI.LikesCount - POI.DislikesCount);
        }
    }
}
