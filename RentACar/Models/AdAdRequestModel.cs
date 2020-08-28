using RentACar.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Entites
{
    public class AdAdRequestModel
    {
        public Guid AdId { get; set; }
        public AdModel Ad { get; set; } // ovo znas
        public Guid AdRequestId { get; set; }
        public AdRequestModel AdRequest { get; set; } //kreira se trenutno
    }
}
