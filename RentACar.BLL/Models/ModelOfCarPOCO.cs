using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.BLL.Models
{
    public class ModelOfCarPOCO
    {
        public Guid Id { get; set; }
        public Guid CarBrandId { get; set; }
        public CarBrandPOCO CarBrend { get; set; }
        public string Name { get; set; }
    }
}
