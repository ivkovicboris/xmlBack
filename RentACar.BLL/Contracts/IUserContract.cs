using RentACar.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.BLL.Contracts
{
    public interface IUserContract
    {
        Task<bool> RegisterUser(RegisterPOCO model);
        Task<object> LoginUser(LoginPOCO loginPOCO);
        Task<bool> SentMailForRegistration(MailPOCO mailModel);
    }
}
