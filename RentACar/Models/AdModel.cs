using RentACar.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class AdModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public CarModel Car { get; set; }
        public int Price { get; set; }
        public bool Cdw { get; set; }
        public ICollection<AdAdRequestModel> AdAdRequests { get; set; }
    }
}
