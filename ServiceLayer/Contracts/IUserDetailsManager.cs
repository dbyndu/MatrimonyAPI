using Matrimony.Model.Base;
using Matrimony.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IUserDetailsManager
    {
        Response GetUserDetails();
    }
}
