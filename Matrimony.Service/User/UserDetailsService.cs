using Matrimony.Data;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Model.User;
using Matrimony.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Matrimony.Service.User
{
    public class UserDetailsService : IUserDetailsService
    {
        private MatrimonyContext _context;
        public UserDetailsService(MatrimonyContext context)
        {
            _context = context;
        }
        public ResponseType GetOneUserDetails(string user)
        {
            var errors = new List<Error>();
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "User Information");
            UserModel model = new UserModel()
            {
                Email = "Srijit.das@gmail.com",
                ContactName = "Srijit",
                FirstName = "Srijit",
                UserID = "4587580"
            };
            return new UserModelResponse(metadata, model);
        }
        public ResponseType GetUserDetails()
        {
            var errors = new List<Error>();
            IQueryable<UserModel> IQueryUsers = null;
            List<UserModel> lstUsers = new List<UserModel>();
            try
            {
                if (!errors.Any())
                {
                    IQueryUsers = _context.User.Select(u => new UserModel { UserID = u.Id.ToString() });
                    lstUsers = IQueryUsers.ToList();
                }
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (lstUsers == null || Convert.ToInt32(lstUsers.Count) == 0)
            {
                errors.Add(new Error("Err102", "No user found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of User.");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new UserModelListResponse(metadata,lstUsers);
        }
    }
}
