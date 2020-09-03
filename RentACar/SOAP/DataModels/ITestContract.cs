using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace RentACar.API.SOAP.DataModels
{
    [ServiceContract]
    public interface ITestContract
    {
        [OperationContract]
        string Ping(string message);
    }
}
