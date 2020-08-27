using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.BLL.Models
{
    public class RegisterPOCO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string UserRole { get; set; }
        public string Jmbg { get; set; }

    }
}
