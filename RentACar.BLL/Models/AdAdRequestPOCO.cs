using RentACar.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Entites
{
    public class AdAdRequestPOCO
    {
        public Guid AdId { get; set; }
        public AdPOCO Ad { get; set; }
        public Guid AdRequestId { get; set; }
        public AdRequestPOCO AdRequest { get; set; }
    }
}
