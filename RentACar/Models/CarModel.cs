using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public ModelOfCarModel Model { get; set; }
        public Guid FuelId { get; set; }
        public FuelTypeModel Fuel { get; set; }
        public int Kilometars { get; set; }
    }
}
