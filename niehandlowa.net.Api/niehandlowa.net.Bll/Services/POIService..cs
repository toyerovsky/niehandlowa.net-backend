using AutoMapper;
using GeoCoordinatePortable;
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
    public class POIService
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
            return _mapper.Map<List<POIModel>>(await _unitOfWork.POIRepository.GetAllAsync()).ToList();
        }

        public async Task<List<POIModel>> GetPOIsByTye(int type)
        {
            return _mapper.Map<List<POIModel>>((await _unitOfWork.POIRepository.FindAllAsync(p => p.Type == type)).ToList());
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
            var poisByType = _mapper.Map<List<POIModel>>(await _unitOfWork.POIRepository.FindAllAsync(p => types.Contains(p.Type))).ToList();
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
    }
}
