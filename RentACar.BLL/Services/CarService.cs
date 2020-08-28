using AutoMapper;
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
    public class CarService : ICarContract
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<CarBrand> _carBrendRepository;
        private readonly IRepository<ModelOfCar> _carModelRepository;
        private readonly IRepository<FuelType> _fuelTypeRepository;
        private readonly IMapper _mapper;
        private readonly RentContext _context;
        private readonly DbSet<Car> _cars;
        private readonly DbSet<ModelOfCar> _models;


        public CarService(IRepository<Car> carRepository,
                            IRepository<CarBrand> carBrendRepository,
                            IRepository<ModelOfCar> carModelRepository,
                            IRepository<FuelType> fuelTypeRepository,
                            IMapper mapper,
                            RentContext context)
        {
            _carRepository = carRepository;
            _carBrendRepository = carBrendRepository;
            _carModelRepository = carModelRepository;
            _fuelTypeRepository = fuelTypeRepository;
            _mapper = mapper;
            _context = context;
            _cars = _context.Cars;
            _models = _context.CarModels;
        }

        public async Task<bool> AddBrand(CarBrandPOCO carBrandPOCO)
        {
            try
            {
                var newBrand = new CarBrand()
                {
                    Id = Guid.NewGuid(),
                    Name = carBrandPOCO.Name

                };

                await _carBrendRepository.Create(newBrand);

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<bool> AddCar(CarPOCO carPOCO)
        {
            try
            {
                carPOCO.Id = Guid.NewGuid();
                Car newCar = _mapper.Map<CarPOCO, Car>(carPOCO);
                await _carRepository.Create(newCar);

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<bool> AddModel(ModelOfCarPOCO carModelPOCO)
        {
            try
            {
                var newModel = new ModelOfCar()
                {
                    Id = Guid.NewGuid(),
                    Name = carModelPOCO.Name,
                    CarBrandId = carModelPOCO.CarBrandId
                    
                };

                await _carModelRepository.Create(newModel);

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<bool> AddType(FuelTypePOCO fuelTypePOCO)
        {
            try
            {
                var newFuelType = new FuelType()
                {
                    Id = Guid.NewGuid(),
                    Name = fuelTypePOCO.Name

                };

                await _fuelTypeRepository.Create(newFuelType);

            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<object> GetAllBrands()
        {
            try
            {
                List<CarBrand> result = _carBrendRepository.GetAll().ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<object> GetAllCars()
        {
            try
            {
                List<Car> result = await _cars.Include(x => x.Model).Include(y => y.Fuel).Include(z => z.Model.CarBrand).ToListAsync();
                //result.ForEach(x => x.Model.CarBrend.ModelsOfBrand = null);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<object> GetAllModels()
        {
            try
            {
                List<ModelOfCar> result = await _models.Include(x => x.CarBrand).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<object> GetAllTypes()
        {
            try
            {
                List<FuelType> result = _fuelTypeRepository.GetAll().ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
