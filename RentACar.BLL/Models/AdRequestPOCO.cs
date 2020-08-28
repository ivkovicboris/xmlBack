using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Entites
{
    public class AdRequestPOCO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RequestStatus Status { get; set; }
        public ICollection<AdAdRequestPOCO> AdAdRequests { get; set; }

    }
}