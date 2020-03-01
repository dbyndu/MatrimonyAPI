using DataAccessLayer.Contracts;
using Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DBContext _dbContext;
        public AuthRepository(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public bool CreateUser(UserBasic user)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExists(string userId)
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(UserBasic user)
        {
            throw new NotImplementedException();
        }
    }
}
