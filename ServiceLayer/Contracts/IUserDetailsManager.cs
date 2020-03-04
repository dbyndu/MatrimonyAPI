using Models.Base;
using Models.UserModels;
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
