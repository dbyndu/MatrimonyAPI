using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.Base;

namespace Matrimony.Service.Contracts
{
    public interface IUserDetailsService
    {
        Response GetUserDetails();
        Response GetOneUserDetails(string user);
    }
}
