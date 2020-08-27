using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Entites
{
    public class Car
    {
        public Guid Id {get; set;}
        public Guid ModelId { get; set; }
        public ModelOfCar Model { get; set; }
        public Guid FuelId { get; set; }
        public FuelType Fuel { get; set; }
        public int Kilometars { get; set; }
    }
}
