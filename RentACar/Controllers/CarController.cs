using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RentACar.API.Models;
using RentACar.BLL.Contracts;
using RentACar.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ICarContract _carContract;

        public CarController(
                             ICarContract carContract,
                             IMapper mapper,
                             IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _carContract = carContract;
        }

        [HttpPost]
        [Route("AddCar")]
        public async Task<bool> AddCar([FromBody] CarModel car)
        {
            try
            {
                var result = await _carContract.AddCar(_mapper.Map<CarModel, CarPOCO>(car));
                if (result)
                {
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        [HttpGet]
        [Route("GetAllCars")]
        public async Task<object> GetAllCars() => await _carContract.GetAllCars();

        [HttpPost]
        [Route("AddBrand")]
        public async Task<bool> AddBrand([FromBody] CarBrandModel brand)
        {
            var result = await _carContract.AddBrand(_mapper.Map<CarBrandModel, CarBrandPOCO>(brand));
            if (result)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("GetAllBrands")]
        public async Task<object> GetAllBrands() => await _carContract.GetAllBrands();

        [HttpPost]
        [Route("AddModel")]
        public async Task<bool> AddModel([FromBody] ModelOfCarModel model)
        {
            var result = await _carContract.AddModel(_mapper.Map<ModelOfCarModel, ModelOfCarPOCO>(model));
            if (result)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("GetAllModels")]
        public async Task<object> GetAllModels()
        {
            var result = await _carContract.GetAllModels();
            return result;
        }

        [HttpPost]
        [Route("AddType")]
        public async Task<bool> AddType([FromBody] FuelTypeModel type)
        {
            var result = await _carContract.AddType(_mapper.Map<FuelTypeModel, FuelTypePOCO>(type));
            if (result)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("GetAllTypes")]
        public async Task<object> GetAllTypes() => await _carContract.GetAllTypes();
    }
}
