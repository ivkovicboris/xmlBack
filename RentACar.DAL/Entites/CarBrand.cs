using System;
using System.Collections.Generic;

namespace RentACar.DAL.Entites
{
    public class CarBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ModelOfCar> ModelsOfBrand { get; set; }
    }
}