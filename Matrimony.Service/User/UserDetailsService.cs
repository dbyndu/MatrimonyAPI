using Matrimony.Data;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Model.User;
using Matrimony.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

namespace Matrimony.Service.User
{
    public class UserDetailsService : IUserDetailsService
    {
        private MatrimonyContext _context;
        private readonly IMapper _mapper;
        public UserDetailsService(MatrimonyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                ID = 4587580
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
                    IQueryUsers = _context.User.Select(u => new UserModel { ID = u.Id });
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
            Matrimony.Data.Entities.User dbUser = new Data.Entities.User()
            {
                Password = user.Password,
                FirstName = "default",
                LastName = "default",
                CreatedDate = DateTime.Now,
                Email = user.Email,
                ProfileCreatedForId = user.ProfileCreatedForId,
                PhoneNumber = user.PhoneNumber
            };
            try
            {
                _context.User.Add(dbUser);
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (outPutResult == 0)
            {
                errors.Add(new Error("Err102", "Can not Add User.."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains User Details Of User");
            if (!errors.Any())
            {
                var insertedUser = _context.User.Where(x => x.Email == user.Email).Select(u => new UserModel
                {
                    ID = u.Id,
                    Email = u.Email,
                    FirstName
                = u.FirstName == "default" ? " " : u.FirstName,
                    LastName
                = u.LastName == "default" ? " " : u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    CreatedDate = u.CreatedDate
                }).FirstOrDefault();
                return new UserModelResponse(metadata, insertedUser);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }
       public Response Register(Object obj, string type)
        {
            var errors = new List<Error>();
            int outPutResult = 0;
            int userId = 0;
            try
            {
                //string objType = obj.GetType().Name;
                switch (type)
                {
                    case "UserRegister":
                        outPutResult = InsertUpdateUserInfo((UserRegister)obj, out userId);
                        break;
                    case "UserBasicInformation":
                        UserBasicInformation userBasic = (UserBasicInformation)obj;
                        outPutResult = InsertUpdateUserBasicInfo(userBasic);
                        userId = userBasic.UserId;
                        break;
                    case "UserFamilyInformationModel":
                        UserFamilyInformationModel userFamily = (UserFamilyInformationModel)obj;
                        outPutResult = InsertUpdateUserFamilyInfo(userFamily);
                        userId = userFamily.UserId;
                        break;
                    case "UserEducationModel":
                        List<UserEducationModel> userEducations = (List<UserEducationModel>)obj;
                        outPutResult = InsertUpdateUserEducation(userEducations);
                        userId = userEducations[0].UserId;
                        break;
                    case "UserCareerModel":
                        List<UserCareerModel> userCarrer = (List<UserCareerModel>)obj;
                        outPutResult = InsertUpdateUserCareer(userCarrer);
                        userId = userCarrer[0].UserId;
                        break;
                    default:
                        // code block
                        break;
                }
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
                //var insertedUser = _context.User.Where(x => x.Id == userId).Select(u => new UserModel
                //{
                //    ID = u.Id,
                //    Email = u.Email,
                //    FirstName
                //= u.FirstName == "default" ? " " : u.FirstName,
                //    LastName
                //= u.LastName == "default" ? " " : u.LastName,
                //    PhoneNumber = u.PhoneNumber,
                //    CreatedDate = u.CreatedDate
                //}).FirstOrDefault();
                //return new UserModelResponse(metadata, insertedUser);
                var insertedUser = _context.User.Where(x => x.Id == userId).FirstOrDefault();
                UserModel userModel = _mapper.Map<UserModel>(insertedUser);

                return new UserModelResponse(metadata, userModel);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }

        private int InsertUpdateUserBasicInfo(UserBasicInformation userBasic)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserBasicInfo dbUserBasic = new Data.Entities.UserBasicInfo()
            {
                Id = userBasic.Id,
                UserId = userBasic.UserId,
                GenderId = userBasic.GenderId,
                Dob = userBasic.Dob,
                About = userBasic.About,
                BloodGroupId = userBasic.BloodGroupId,
                ComunityId = userBasic.ComunityId,
                MaritalStatusId = userBasic.MaritalStatusId,
                Height = userBasic.Height,
                Weight = userBasic.Weight,
                HealthInfoId = userBasic.HealthInfoId,
                IsDisability = userBasic.IsDisability,
                ReligionId = userBasic.ReligionId,
                MotherTongueId = userBasic.MotherTongueId,
                Gothra = userBasic.Gothra,
                IsIgnorCast = userBasic.IsIgnorCast
            };
            try
            {
                if (userBasic.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserBasicInfo>(dbUserBasic);
                }
                else
                {
                    _context.UserBasicInfo.Add(dbUserBasic);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }

        private int InsertUpdateUserFamilyInfo(UserFamilyInformationModel userFamily)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserFamilyInfo dbUserFamilyInfo = _mapper.Map<Matrimony.Data.Entities.UserFamilyInfo>(userFamily);
            //Matrimony.Data.Entities.UserFamilyInfo dbUserFamilyInfo = new Data.Entities.UserFamilyInfo()
            //{
            //    Id = userBasic.Id,
            //    UserId = userBasic.UserId,
            //    FatherStatusId = userBasic.GenderId,
            //    MotherStatusId = userBasic.Dob,
            //    NativePlace = userBasic.About,
            //    CreatedDate = userBasic.BloodGroupId,
            //    ModifiedDate = userBasic.ComunityId,
            //    FamilyLocation = userBasic.MaritalStatusId,
            //    MarriedSiblingMale = userBasic.Height,
            //    NotMarriedSiblingMale = userBasic.Weight,
            //    MarriedSiblingFemale = userBasic.HealthInfoId,
            //    NotMarriedSiblingFemale = userBasic.IsDisability,
            //    FamilyTypeId = userBasic.ReligionId,
            //    FamilyValuesId = userBasic.MotherTongueId,
            //    FamilyAffluenceId = userBasic.Gothra
            //};
            try
            {
                if (dbUserFamilyInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserFamilyInfo>(dbUserFamilyInfo);
                }
                else
                {
                    _context.UserFamilyInfo.Add(dbUserFamilyInfo);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        private int InsertUpdateUserEducation(List<UserEducationModel> UserEducations)
        {
            int outPutResult = 0;
            try
            {
                foreach (UserEducationModel userEducation in UserEducations) {
                    Matrimony.Data.Entities.UserEducation dbUserEdu = new Data.Entities.UserEducation()
                    {
                        Id = userEducation.Id,
                        UserId = userEducation.UserId,
                        EducationLevelId = userEducation.EducationLevelId,
                        EducationFieldId = userEducation.EducationFieldId,
                        Institution = userEducation.Institution,
                        University = userEducation.University
                    };
                    if (userEducation.Id > 0)
                    {
                        _context.Update<Matrimony.Data.Entities.UserEducation>(dbUserEdu);
                    }
                    else 
                    {
                        _context.UserEducation.Add(dbUserEdu);
                    }                    
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        //public int CreateNewUser(UserShortRegister user)
        //{
        //    int outPutResult = 0;
        //    try
        //    {
        //        Matrimony.Data.Entities.User dbUser = new Data.Entities.User()
        //        {
        //            Password = user.Password,
        //            FirstName = "default",
        //            LastName = "default",
        //            CreatedDate = DateTime.Now,
        //            Email = user.Email,
        //            ProfileCreatedForId = user.ProfileCreatedForId,
        //            PhoneNumber = user.PhoneNumber
        //        };
        //        _context.User.Add(dbUser);
        //        outPutResult = _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return outPutResult;
        //}
        private int InsertUpdateUserInfo(UserRegister user, out int userId)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.User dbUser = null;
            try
            {
                if (user.ID > 0)
                {
                    dbUser = new Data.Entities.User
                    {
                        Id = user.ID,
                        FirstName = user.FirstName,
                        MiddleNmae = user.MiddleNmae,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        ProfileCreatedForId = user.ProfileCreatedForId,
                        ContactName = user.ContactName,
                        UpdatedDate = DateTime.Now
                    };
                    _context.Entry(dbUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Entry(dbUser).Property(x => x.CreatedDate).IsModified = false;
                    _context.Entry(dbUser).Property(x => x.Password).IsModified = false;
                    //_context.Update<Matrimony.Data.Entities.User>(dbUser);
                }
                else
                {
                    dbUser = new Data.Entities.User()
                    {
                        Password = user.Password,
                        FirstName = user.FirstName,
                        MiddleNmae = user.MiddleNmae,
                        LastName = user.LastName,
                        Email = user.Email,
                        ProfileCreatedForId = user.ProfileCreatedForId,
                        PhoneNumber = user.PhoneNumber,
                        ContactName = user.ContactName
                    };
                    _context.User.Add(dbUser);
                }
                
                outPutResult = _context.SaveChanges();

                userId = dbUser.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }

        private int InsertUpdateUserCareer(List<UserCareerModel> userCareers)
        {
            int outPutResult = 0;
            try
            {
                foreach (UserCareerModel userCareer in userCareers)
                {
                    Matrimony.Data.Entities.UserCareer dbUserCareer = new Data.Entities.UserCareer()
                    {
                        Id = userCareer.Id,
                        UserId = userCareer.UserId,
                        WorkingSectorId = userCareer.WorkingSectorId,
                        WorkDesignationId = userCareer.WorkDesignationId,
                        EmployerId = userCareer.EmployerId,
                        AnualIncomeId = userCareer.AnualIncomeId,
                        IsDisplayIncome = userCareer.IsDisplayIncome

                    };
                    if (userCareer.Id > 0)
                    {
                        _context.Update<Matrimony.Data.Entities.UserCareer>(dbUserCareer);
                    }
                    else
                    {
                        _context.UserCareer.Add(dbUserCareer);
                    }
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }

    }
}
