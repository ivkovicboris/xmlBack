using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.BLL.Models
{
    public class CarBrandPOCO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ModelOfCarPOCO> ModelsOfBrand {get; set;}
    }
}
