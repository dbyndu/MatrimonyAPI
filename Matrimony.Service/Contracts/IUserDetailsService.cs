using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.Base;
using Matrimony.Model.User;

namespace Matrimony.Service.Contracts
{
    public interface IUserDetailsService
    {
        Response GetUserDetails();
        Response GetOneUserDetails(string user);

        Response CreateNewUser(UserShortRegister user);
        Response Register(Object obj, string type);
        Response GetImages(int userId, int width, int height);
        Response GestUserList();
    }
}
