using Matrimony.Model.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Data.Contracts
{
    public interface IAuthRepository
    {
        bool CreateUser(UserRegister user);
        bool IsUserExists(string userId);
        bool LoginUser(UserRegister user);
    }
}
