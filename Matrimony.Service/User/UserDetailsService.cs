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
using Microsoft.EntityFrameworkCore;
using Matrimony.Helper;

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
                PhoneNumber = user.PhoneNumber,
                ContactName = user.Email + "_" + user.PhoneNumber
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
                    CreatedDate = u.CreatedDate,
                    ContactName =u.ContactName
                }).FirstOrDefault();
                return new UserModelResponse(metadata, insertedUser);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }

        public Response GestUserList()
        {
            var errors = new List<Error>();
            var lstUsers = (from u in _context.User
                            join ui in _context.UserInfo on u.Id equals ui.UserId into user_basic
                            from ub in user_basic.DefaultIfEmpty()
                            //join ul in _context.UserLocation on u.Id equals ul.UserId into user_loc
                            //from uloc in user_loc.DefaultIfEmpty()
                            //join ue in _context.UserEducation on u.Id equals ue.Id into user_edu
                            //from uedu in user_edu.DefaultIfEmpty()
                            //join uc in _context.UserCareer on u.Id equals uc.Id into user_career
                            //from ucar in user_career.DefaultIfEmpty()
                            join mLang in _context.MasterFieldValue on ub.MotherTongueId equals mLang.Id into language
                            from l in language.DefaultIfEmpty()
                            join mEdu in _context.MasterFieldValue on ub.HighestQualificationId equals mEdu.Id into highstEducation
                            from he in highstEducation.DefaultIfEmpty()
                            join mEduField in _context.MasterFieldValue on ub.HighestSpecializationId equals mEduField.Id into highstEducationField
                            from hef in highstEducationField.DefaultIfEmpty()
                            join mWork in _context.MasterFieldValue on ub.WorkDesignationId equals mWork.Id into workDesignation
                            from w in workDesignation.DefaultIfEmpty()
                            join mCity in _context.MasterFieldValue on ub.CityId equals mCity.Id into city
                            from c in city.DefaultIfEmpty()
                            select new
                            {
                                Id = u.Id,
                                Name = string.Concat(u.FirstName, " ", u.MiddleNmae, " ", u.LastName),
                                Age = GenericHelper.CalculateAge(Convert.ToDateTime(ub.Dob)),
                                Height = ub.Height,
                                Education = string.Concat(he.Value ?? string.Empty, ", ", hef.Value ?? string.Empty),
                                City = c.Value ?? string.Empty,
                                Profession = w.Value ?? string.Empty,
                                Language = l.Value ?? string.Empty,
                                Url = ""
                            }).ToList();
            if (lstUsers == null || Convert.ToInt32(lstUsers.Count) == 0)
            {
                errors.Add(new Error("Err102", "No user found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains list Of User");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new AnonymousResponse(metadata, lstUsers);
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
                    case "UserImage":
                        UserImage userImage = (UserImage)obj;
                        outPutResult = InsertUpdateUserImage(userImage);
                        userId = userImage.UserId;
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

        public Response GetImages(int userId, int width, int height, string mode)
        {
            var errors = new List<Error>();
            IQueryable<UserImage> IQueryImages = null;
            List<UserImage> lstImages = new List<UserImage>();
            try
            {
                IQueryImages = _context.UserImage.Where(i => i.UserId == userId).Select(u => new UserImage
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    ImageString = "data:" + u.ContentType + ";base64," + GenericHelper.ResizeImage((byte[])u.Image, width, height, mode) // ImageResizer((byte[])u.Image, width, height)
                });
                lstImages = IQueryImages.ToList();
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (lstImages == null || Convert.ToInt32(lstImages.Count) == 0)
            {
                errors.Add(new Error("Err102", "No iage found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains Images Of User");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new UserImageListResponse(metadata, lstImages);
        }
        private int InsertUpdateUserImage(UserImage userImg)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserImage dbUserImage = new Data.Entities.UserImage()
            {
                Id = userImg.Id,
                UserId = userImg.UserId,
                Image = userImg.Image,
                ContentType = userImg.ContentType
            };
            try
            {
                if (userImg.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserImage>(dbUserImage);
                }
                else
                {
                    _context.UserImage.Add(dbUserImage);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        private int InsertUpdateUserBasicInfo(UserBasicInformation userBasic)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserInfo dbUserBasic = new Data.Entities.UserInfo()
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
                BodyTypeId = userBasic.HealthInfoId,
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
                    _context.Update<Matrimony.Data.Entities.UserInfo>(dbUserBasic);
                }
                else
                {
                    _context.UserInfo.Add(dbUserBasic);
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
            Matrimony.Data.Entities.UserInfo dbUserFamilyInfo = _mapper.Map<Matrimony.Data.Entities.UserInfo>(userFamily);
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
                    _context.Update<Matrimony.Data.Entities.UserInfo>(dbUserFamilyInfo);
                }
                else
                {
                    _context.UserInfo.Add(dbUserFamilyInfo);
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
                    Matrimony.Data.Entities.UserInfo dbUserEdu = new Data.Entities.UserInfo()
                    {
                        Id = userEducation.Id,
                        UserId = userEducation.UserId,
                        HighestQualificationId = userEducation.EducationLevelId,
                        HighestSpecializationId = userEducation.EducationFieldId,
                        Institution = userEducation.Institution,
                        University = userEducation.University
                    };
                    if (userEducation.Id > 0)
                    {
                        _context.Update<Matrimony.Data.Entities.UserInfo>(dbUserEdu);
                    }
                    else 
                    {
                        _context.UserInfo.Add(dbUserEdu);
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
                    Matrimony.Data.Entities.UserInfo dbUserCareer = new Data.Entities.UserInfo()
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
                        _context.Update<Matrimony.Data.Entities.UserInfo>(dbUserCareer);
                    }
                    else
                    {
                        _context.UserInfo.Add(dbUserCareer);
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
