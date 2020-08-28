using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentACar.BLL.Models;
using RentACar.DAL.Entites;

namespace RentACar.BLL.Contracts
{
    public interface IAdContract
    {
        Task<List<Ad>> GetAllAds();
        Task<bool> AddAd(AdPOCO adPOCO);
        Task<object> GetAllAdsByUserId(Guid guid);
        Task<object> GetFreeAdsByDate(DateTime startDate, DateTime endDate);
    }
}
