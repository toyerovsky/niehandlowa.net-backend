using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using niehandlowa.net.Bll.Models;

namespace niehandlowa.net.Bll.Services
{
    public interface IPOIService
    {
        Task Create(POIModel model);
        Task Delete(int id);
        Task<List<POIModel>> GetAllPOIs();
        Task<List<POIModel>> GetPOIsByType(int type);
        Task<List<POIModel>> GetPOIsWithinDistance(double latitude, double longitude, int distance);
        Task<List<POIModel>> GetPOIsWithinDistanceByTypesList(double latitude, double longitude, int distance, List<int> types);
        Task<bool> IsPOIOpenAtTime(int POIId, DateTime date, bool? tradeSunday);
        Task Update(POIModel model);
        Task<List<POIModel>> GetPOIsByTypesList(List<int> types);
        Task<List<POIModel>> GetNowOpenPOIs();
        Task GiveLikeForPOI(int id);
        Task GiveDislikeForPOI(int id);
        Task<int> GetDifferenceLikesForPOI(int id);
    }
}