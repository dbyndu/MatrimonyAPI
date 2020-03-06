using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.User;

namespace Matrimony.Service.Contracts
{
    public interface IAuthService
    {
        bool LoginUser(UserBasicModel user);
        bool RegisterUser(UserBasicModel user);
    }
}
