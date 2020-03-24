using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matrimony.Model.User;

namespace Matrimony.Service.Contracts
{
    public interface IAuthService
    {
        bool LoginUser(UserRegister user);
        bool RegisterUser(UserRegister user);
    }
}
