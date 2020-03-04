using System;
using System.Collections.Generic;
using System.Text;
using Models.Base;

namespace Matrimony.Service.Contracts
{
    public interface IUserDetailsService
    {
        Response GetUserDetails();
    }
}
