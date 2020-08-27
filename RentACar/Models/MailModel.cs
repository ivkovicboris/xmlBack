﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Models
{
    public class MailModel
    {
        public string Subject { get; set; }

        public string Sender { get; set; }
        public List<string> Receivers { get; set; }
        public string Body { get; set; }
    }
}
