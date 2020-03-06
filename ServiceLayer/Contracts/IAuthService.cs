﻿using Matrimony.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IAuthService
    {
        bool LoginUser(UserBasicModel user);
        bool RegisterUser(UserBasicModel user);
    }
}
