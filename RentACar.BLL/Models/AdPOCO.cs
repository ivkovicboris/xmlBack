using RentACar.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.BLL.Models
{
    public class AdPOCO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public CarPOCO Car { get; set; }
        public int Price { get; set; }
        public bool Cdw { get; set; }
        public ICollection<AdAdRequestPOCO> AdAdRequests { get; set; }
    }
}
