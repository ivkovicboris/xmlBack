using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RentACar.BLL.Models;

namespace RentACar.BLL.Contracts
{
    public interface ICarContract
    {
        Task<bool> AddCar(CarPOCO carPOCO);
        Task<bool> AddBrand(CarBrandPOCO carBrandPOCO);
        Task<bool> AddModel(ModelOfCarPOCO carModelPOCO);
        Task<bool> AddType(FuelTypePOCO fuelTypePOCO);
        Task<object> GetAllBrands();
        Task<object> GetAllModels();
        Task<object> GetAllTypes();
        Task<object> GetAllCars();
    }
}
