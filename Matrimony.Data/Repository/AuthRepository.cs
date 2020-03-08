using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matrimony.Data;
using Matrimony.Data.Contracts;
using Matrimony.Model.User;

namespace Matrimony.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MatrimonyContext _dbContext;
        public AuthRepository(MatrimonyContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public bool CreateUser(UserBasicModel user)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExists(string userId)
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(UserBasicModel user)
        {
            throw new NotImplementedException();
        }
    }
}
