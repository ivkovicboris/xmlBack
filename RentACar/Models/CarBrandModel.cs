using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class CarBrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ModelOfCarModel> ModelsOfBrand { get; set; }
    }
}
