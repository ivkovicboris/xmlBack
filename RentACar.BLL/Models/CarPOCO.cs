using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.BLL.Models
{
    public class CarPOCO
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public ModelOfCarPOCO Model { get; set; }
        public Guid FuelId { get; set; }
        public FuelTypePOCO Fuel { get; set; }
        public int Kilometars { get; set; }
    }
}
