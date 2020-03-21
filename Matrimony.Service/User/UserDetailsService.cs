using Matrimony.Data;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Model.User;
using Matrimony.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Matrimony_Model.User;

namespace Matrimony.Service.User
{
    public class UserDetailsService : IUserDetailsService
    {
        private MatrimonyContext _context;
        public UserDetailsService(MatrimonyContext context)
        {
            _context = context;
        }
        public Response GetOneUserDetails(string user)
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
        public Response GetUserDetails()
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

        public Response CreateNewUser(UserShortRegister user)
        {
            var errors = new List<Error>();
            int outPutResult = 0;
            Matrimony.Data.Entities.User dbUser = new Data.Entities.User() { Password = user.password, FirstName = "default", LastName = "default", 
                CreatedDate = DateTime.Now, Email = user.email, ProfileCreatedForId = user.profile, PhoneNumber = user.phone };
            try
            {
                _context.User.Add(dbUser);
                outPutResult = _context.SaveChanges();
            }
            catch(Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }            
            if(outPutResult == 0)
            {
                errors.Add(new Error("Err102", "Can not Add User.."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains User Details Of User");
            if (!errors.Any())
            {
                var insertedUser = _context.User.Where(x => x.Email == user.email).Select(u => new UserModel { UserID = u.Id.ToString(), Email = u.Email,FirstName
                = u.FirstName == "default"? " " : u.FirstName, LastName
                =u.LastName == "default" ? " " : u.LastName, PhoneNumber = u.PhoneNumber, CreatedDate = u.CreatedDate
                }).FirstOrDefault();
                return new UserModelResponse(metadata, insertedUser);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }
    }
}
