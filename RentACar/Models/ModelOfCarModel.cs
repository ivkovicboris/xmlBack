using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class ModelOfCarModel
    {
        public Guid Id { get; set; }
        public Guid CarBrandId { get; set; }
        public CarBrandModel CarBrand { get; set; }
        public string Name { get; set; }
    }
}
