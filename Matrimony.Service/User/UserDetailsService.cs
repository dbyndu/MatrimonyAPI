using Matrimony.Data;
using Matrimony.Service.Contracts;
using Models.Base;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Service.User
{
    public class UserDetailsService : IUserDetailsService
    {
        private MatrimonyContext _context;
        public UserDetailsService(MatrimonyContext context)
        {
            _context = context;
        }
        public Response GetUserDetails()
        {
            var errors = new List<Error>();
            var metadata = new Metadata(true, "1233546hgfghff", "hgghggh");
            List<Models.UserModels.User> ListUser = new List<Models.UserModels.User>();
            Models.UserModels.UserListResponse userlistResponse = new Models.UserModels.UserListResponse(metadata,ListUser);
            return userlistResponse;

        }
    }
}
