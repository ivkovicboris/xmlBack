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
        private readonly IRepository<AdRequest> _adRequestRepository;
        private readonly IRepository<AdAdRequest> _adAdRequestRepository;
        private readonly DbSet<Car> _cars;
        private readonly DbSet<AdAdRequest> _adAdRequests;
        private readonly RentContext _context;


        public AdService(IRepository<Ad> adRepository,
                 IRepository<AdRequest> adRequestRepository,
                 IRepository<AdAdRequest> adAdRequestRepository,
                 UserManager<User> userManager,
                 IMapper mapper,
                 RentContext context
            )
        {
            _adRepository = adRepository;
            _adRequestRepository = adRequestRepository;
            _adAdRequestRepository = adAdRequestRepository;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _ads = _context.Ads;
            _cars = _context.Cars;
            _adAdRequests = _context.AdAdRequests;


        }

        public async Task<bool> AcceptAdRequest(AdAdRequestPOCO adAdRequestPOCO)
        {
            var adRequests = await _adRequestRepository.Find(x => x.Id.Equals(adAdRequestPOCO.AdRequestId));
            var adRequest = adRequests.FirstOrDefault();
            adRequest.Status = RequestStatus.Accepted;
            await _adRequestRepository.Update(adRequest);
            ///////////////////////////////////////////
            var w = adAdRequestPOCO.AdRequest;
            var z = await _adAdRequestRepository.Find(q => q.AdRequest.Equals(w));
            var listofadaddrequestfordelete = z.ToList();
            foreach (var adad in listofadaddrequestfordelete)
            {
                var a = await _adRepository.Find(y => y.Id.Equals(adad.AdId));
                var b = await _adAdRequestRepository.Find(x => x.Ad.Equals(a.FirstOrDefault()));
                List<Guid> aar = new List<Guid>();
                foreach (var d in b) // d ima sve sem adRequsta
                {
                    aar.Add(d.AdRequestId);
                }
                foreach (var c in aar)
                {
                    var ar = await _adRequestRepository.Find(x => x.Id.Equals(c) && x.Status == RequestStatus.Requested);
                    await _adRequestRepository.Delete(ar.FirstOrDefault());
                }

            }

            return true;
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

        public async Task<bool> AddAdRequest(AdRequestPOCO adRequestPOCO)
        {
            try
            {
                // add addrequest
                adRequestPOCO.Id = Guid.NewGuid();
                //adRequestPOCO.Status = RequestStatus.Requested;
                AdRequest newAdRequest = _mapper.Map<AdRequestPOCO, AdRequest>(adRequestPOCO);
                newAdRequest.AdAdRequests = null;
                await _adRequestRepository.Create(newAdRequest);

                //addad
                var listadad = new List<AdAdRequest>();
                var listofAds = new List<Ad>();
                
                var listofadaddrequest = adRequestPOCO.AdAdRequests.ToList();
                foreach (var adad in listofadaddrequest)
                {
                    var a = await _adRepository.Find(y => y.Id.Equals(adad.AdId));
                    a.FirstOrDefault();
                    listofAds.Add(a.FirstOrDefault());

                }
               // listofadaddrequest.ForEach(async x =>
                //{
                //    var entity = await _adRepository.Find(y => y.Id.Equals(x.AdId));
                //    var a = entity;
                //    listofAds.Add(entity.FirstOrDefault());
                //});
                listofAds.ForEach(x =>
                {
                    listadad.Add(new AdAdRequest()
                    {
                        Ad = x,
                        AdRequest = newAdRequest
                    });
                });
                    //await _adAdRequestRepository.Create(new AdAdRequest()
                    //{
                    //    Ad = entity,
                    //    AdRequest = newAdRequest
                    //});
                
                listadad.ForEach(x => _adAdRequestRepository.Create(x));

                

                //List<AdAdRequest> list1 = new List<AdAdRequest>();
                //List<Ad> listAd = new List<Ad>();
                //List<AdRequest> listAdRequest = new List<AdRequest>();
                //list.ForEach(x => listAd.Add(x.Ad));
                //foreach(var ad in listAd)
                //{
                //    AdAdRequest adAdRequest = new AdAdRequest();
                //    adAdRequest.Ad = ad;
                //    adAdRequest.AdRequest = newAdRequest;
                //    await _adAdRequestRepository.Create(adAdRequest);
                //}
                //list.ForEach(x => x.Ad.Id = new Guid());
                //list.ForEach(x => _adAdRequestRepository.Create(x));

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<bool> BookAdByAdmin(AdRequestPOCO adRequestPOCO)
        {
            try
            {
                var listofadaddrequestfordelete = adRequestPOCO.AdAdRequests.ToList();
                foreach (var adad in listofadaddrequestfordelete)
                {
                    var a = await _adRepository.Find(y => y.Id.Equals(adad.AdId));
                    var b = await _adAdRequestRepository.Find(x => x.Ad.Equals(a.FirstOrDefault()));
                    List<Guid> aar = new List<Guid>();
                    foreach( var d in b) // d ima sve sem adRequsta
                    {
                        aar.Add(d.AdRequestId);
                    }
                    foreach(var c in aar)
                    {
                        var ar = await _adRequestRepository.Find(x => x.Id.Equals(c) && x.Status==RequestStatus.Requested);
                        await _adRequestRepository.Delete(ar.FirstOrDefault());
                    }

                }
                adRequestPOCO.Id = Guid.NewGuid();
                adRequestPOCO.Status = RequestStatus.Accepted;
                AdRequest newAdRequest = _mapper.Map<AdRequestPOCO, AdRequest>(adRequestPOCO);
                newAdRequest.AdAdRequests = null;
                await _adRequestRepository.Create(newAdRequest);

                var listadad = new List<AdAdRequest>();
                var listofAds = new List<Ad>();

                var listofadaddrequest = adRequestPOCO.AdAdRequests.ToList();
                foreach (var adad in listofadaddrequest)
                {
                    var a = await _adRepository.Find(y => y.Id.Equals(adad.AdId));
                    a.FirstOrDefault();
                    listofAds.Add(a.FirstOrDefault());

                }

                listofAds.ForEach(x =>
                {
                    listadad.Add(new AdAdRequest()
                    {
                        Ad = x,
                        AdRequest = newAdRequest
                    });
                });

                listadad.ForEach(x => _adAdRequestRepository.Create(x));

                

                }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<bool> FinishRent(AdAdRequestPOCO adAdRequestPOCO)
        {
            var ar = await _adRequestRepository.Find(x => x.Id.Equals(adAdRequestPOCO.AdRequestId));
            ar.FirstOrDefault().Status = RequestStatus.Finished;
            return await _adRequestRepository.Update(ar.FirstOrDefault());
        }

        public async Task<object> GetAllAdAccepted()
        {
            var adRequests = await _adRequestRepository.Find(x => x.Status.Equals(RequestStatus.Accepted));
            List<AdRequest> adReq = adRequests.ToList();
            List<AdAdRequest> adAdRequests = new List<AdAdRequest>();
            foreach (var a in adReq)
            {
                adAdRequests = await _adAdRequests.Where(x => x.AdRequestId.Equals(a.Id)).
                    Include(y => y.Ad).
                    Include(z => z.AdRequest).
                    Include(r => r.Ad.Car).
                    Include(k => k.Ad.Car.Fuel).
                    Include(s => s.Ad.Car.Model).
                    Include(o => o.Ad.Car.Model.CarBrand).ToListAsync();
            }
            return adAdRequests;
        }

        public async Task<object> GetAllAdRequests()
        {
            var adRequests = await _adRequestRepository.Find(x => x.Status.Equals(RequestStatus.Requested));
            List<AdRequest> adReq = adRequests.ToList();
            List<AdAdRequest> adAdRequests = new List<AdAdRequest>();
            foreach (var a in adReq)
            {
                adAdRequests = await _adAdRequests.Where(x => x.AdRequestId.Equals(a.Id)).
                    Include(y => y.Ad).
                    Include(z => z.AdRequest).
                    Include(r => r.Ad.Car).
                    Include(k => k.Ad.Car.Fuel).
                    Include(s => s.Ad.Car.Model).
                    Include(o => o.Ad.Car.Model.CarBrand).ToListAsync();
            }
            return adAdRequests;
        }

        public async Task<object> GetAllAds()
        {
            try
            {
                List<Ad> result = await _ads.Include(x => x.Car).Include(y => y.Car.Fuel).Include(z => z.Car.Model).Include(r => r.Car.Model.CarBrand).ToListAsync();
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
            return await _adRepository.Find(x => x.UserId.Equals(userId));
            
        }
    }
}
