using Matrimony.Data;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Model;
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
            List<Model.User.UserModel> ListUser = new List<Model.User.UserModel>();
            Model.User.UserModelListResponse userlistResponse = new Model.User.UserModelListResponse(metadata,ListUser);
            return userlistResponse;

        }
    }
}
