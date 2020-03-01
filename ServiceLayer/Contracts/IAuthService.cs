using Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IAuthService
    {
        bool LoginUser(UserBasic user);
        bool RegisterUser(UserBasic user);
    }
}
