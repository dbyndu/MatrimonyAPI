using Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts
{
    public interface IAuthRepository
    {
        bool CreateUser(UserBasic user);
        bool IsUserExists(string userId);
        bool LoginUser(UserBasic user);
    }
}
