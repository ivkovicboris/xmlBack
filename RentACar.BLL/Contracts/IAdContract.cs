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
        Task<object> GetAllAds();
        Task<bool> AddAd(AdPOCO adPOCO);
        Task<object> GetAllAdsByUserId(Guid guid);
        Task<bool> AddAdRequest(AdRequestPOCO adRequestPOCO);
        Task<object> GetAllAdRequests();
    }
}
