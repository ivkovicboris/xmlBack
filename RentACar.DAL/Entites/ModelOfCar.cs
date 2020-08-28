using System;

namespace RentACar.DAL.Entites
{
    public class ModelOfCar
    {
        public Guid Id { get; set; }
        public Guid CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
        public string Name { get; set; }
    }
}