using AutoMapper;
using RentACar.API.Models;
using RentACar.BLL.Models;
using RentACar.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelOfCar = RentACar.DAL.Entites.ModelOfCar;

namespace RentACar.API.Services
{
    public class SetupAutoMapper : Profile
    {
        public SetupAutoMapper()
        {
            //relation API <=> BLL
            CreateMap<RegisterModel, RegisterPOCO>();
            CreateMap<RegisterPOCO, RegisterModel>();
            CreateMap<LoginModel, LoginPOCO>();
            CreateMap<LoginPOCO, LoginModel>();
            CreateMap<ClientModel, ClientPOCO>();
            CreateMap<ClientPOCO, ClientModel>();
            CreateMap<AgentModel, AgentPOCO>();
            CreateMap<AgentPOCO, AgentModel>();
            CreateMap<MailModel, MailPOCO>();
            CreateMap<MailPOCO, MailModel>();
            CreateMap<CarModel, CarPOCO>();
            CreateMap<CarPOCO, CarModel>();
            CreateMap<CarBrandModel, CarBrandPOCO>();
            CreateMap<CarBrandPOCO, CarBrandModel>();
            CreateMap<ModelOfCarModel, ModelOfCarPOCO>();
            CreateMap<ModelOfCarPOCO, ModelOfCarModel>();
            CreateMap<FuelTypeModel, FuelTypePOCO>();
            CreateMap<FuelTypePOCO, FuelTypeModel>();
            CreateMap<AdModel, AdPOCO>();
            CreateMap<AdPOCO, AdModel>();
            CreateMap<AdRequestModel, AdRequestPOCO>();
            CreateMap<AdRequestPOCO, AdRequestModel>();
            CreateMap<AdAdRequestModel, AdAdRequestPOCO>();
            CreateMap<AdAdRequestPOCO, AdAdRequestModel>();

            //relation BLL <=> DAL
            CreateMap<RegisterPOCO, User>();
            CreateMap<User, RegisterPOCO>();
            CreateMap<ClientPOCO, Client>();
            CreateMap<Client, ClientPOCO>();
            CreateMap<AgentPOCO, Agent>();
            CreateMap<Agent, AgentPOCO>();
            CreateMap<CarPOCO, Car>();
            CreateMap<Car, CarPOCO>();
            CreateMap<CarBrandPOCO, CarBrand>();
            CreateMap<CarBrand, CarBrandPOCO>();
            CreateMap<ModelOfCarPOCO, ModelOfCar>();
            CreateMap<ModelOfCar, ModelOfCarPOCO>();
            CreateMap<FuelTypePOCO, FuelType>();
            CreateMap<FuelType, FuelTypePOCO>();
            CreateMap<AdPOCO, Ad>();
            CreateMap<Ad, AdPOCO>();
            CreateMap<AdRequestPOCO, AdRequest>();
            CreateMap<AdRequest, AdRequestPOCO>();
            CreateMap<AdAdRequestPOCO, AdAdRequest>();
            CreateMap<AdAdRequest, AdAdRequestPOCO>();
        }
    }
}
