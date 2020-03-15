using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.Base;
using Matrimony_Model.User;

namespace Matrimony.Service.Contracts
{
    public interface IUserDetailsService
    {
        Response GetUserDetails();
        Response GetOneUserDetails(string user);

        Response CreateNewUser(UserShortRegister user);
    }
}
