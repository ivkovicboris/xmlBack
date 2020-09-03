using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace RentACar.API.SOAP.DataModels
{
    public class TestContract : ITestContract
    {

        public string Ping(string message)
        {
            return "Zopp";
        }
    }
}
