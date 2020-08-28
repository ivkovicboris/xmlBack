using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.BLL.Contracts;
using RentACar.BLL.Models;
using RentACar.DAL.Context;
using RentACar.DAL.Entites;
using RentACar.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.BLL.Services
{
    public class AdService : IAdContract
    {
        private readonly IRepository<Ad> _adRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DbSet<Ad> _ads;
        private readonly DbSet<Car> _cars;
        private readonly RentContext _context;


        public AdService(IRepository<Ad> adRepository,
                 UserManager<User> userManager,
                 IMapper mapper,
                 RentContext context
            )
        {
            _adRepository = adRepository;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _ads = _context.Ads;
            _cars = _context.Cars;
            
        }

        public async Task<bool> AddAd(AdPOCO adPOCO)
        {
            try
            {
                adPOCO.Id = Guid.NewGuid();
                Ad newAd = _mapper.Map<AdPOCO, Ad>(adPOCO);
                await _adRepository.Create(newAd);

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<List<Ad>> GetAllAds()
        {
            try
            {
                List<Ad> result = await _ads.Include(x => x.Car).Include(y => y.Car.Fuel).Include(z => z.Car.Model).Include(r => r.Car.Model.CarBrend).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<object> GetAllAdsByUserId(Guid userId)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            return _adRepository.Find(x => x.UserId.Equals(userId)).ToList();
            
        }


        public async Task<object> GetFreeAdsByDate(DateTime startDate, DateTime endDate)
        {
            List<Ad> result = await GetAllAds();
            
        NEXT_AD:
            foreach (Ad ad in result)
            {
                foreach (AdAdRequest adadRequest in _adAdRequestRepository.Find(x => x.AdId.Equals(ad.Id)).ToList())
                {
                    foreach (AdRequest adRequest in _adRequestRepository.Find(adreq => adreq.Id.Equals(adadRequest.Id)).ToList())
                    {
                        if (adRequest.Status.Equals(Accepted))
                        {
                            if ((startDate < adRequest.StartDate && endDate > adRequest.EndDate) ||
                               (isDateBetweenTwoDates(startDate, adRequest.StartDate, adRequest.EndDate) || isDateBetweenTwoDates(endDate, adRequest.StartDate, adRequest.EndDate)))
                            {
                                result.Remove(ad);
                                goto NEXT_AD;
                            }
                        }
                    }
                    result.Add(ad);

                }
            }
            return result;
        }

        public static bool isDateBetweenTwoDates(this DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }
    }
}
