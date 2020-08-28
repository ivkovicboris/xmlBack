using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Entites
{
    public class AdAdRequest
    {
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
        public Guid AdRequestId { get; set; }
        public AdRequest AdRequest { get; set; }
    }
}
