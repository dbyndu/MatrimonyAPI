﻿using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Data.Contracts;
using Matrimony.Model.User;
using Matrimony.Service.Contracts;

namespace Matrimony.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _iAuthRepository;
        public AuthService(IAuthRepository authRepository)
        {
            this._iAuthRepository = authRepository;
        }
        public bool RegisterUser(UserBasicModel user)
        {
            if (!_iAuthRepository.CreateUser(user))
            {
                throw new Exception($"This userId {user.UserID} already in use");
            }
            else
                return true;
        }
        public bool LoginUser(UserBasicModel user)
        {
            if (!_iAuthRepository.LoginUser(user))
            {
                throw new Exception($"Invalid user id or password");
            }
            else
                return true;
        }
    }
}
