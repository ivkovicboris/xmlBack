using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class AgentModel
    {
        public Guid AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pib { get; set; }
        public string BankAccNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
