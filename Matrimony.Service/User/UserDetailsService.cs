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
using System.Threading.Tasks;

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
            return new UserModelListResponse(metadata, lstUsers);
        }
        public Response GetUserDetails(int id)
        {
            var errors = new List<Error>();
            UserModel lstUsers = new UserModel();
            try
            {
                if (!errors.Any())
                {
                    //IQueryUsers = _context.User.Where(u => u.Id.Equals(id)).Select(u => new UserModel
                    //{
                    //    ID = u.Id,
                    //    Email = u.Email,
                    //    FirstName = u.FirstName,
                    //    LastName = u.LastName,
                    //    MiddleNmae = u.MiddleNmae,
                    //    PhoneNumber = u.PhoneNumber,
                    //    ProfileCreatedForId = u.ProfileCreatedForId
                    //}) ;
                    lstUsers = GetUserInformation(id);
                }
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (lstUsers == null)
            {
                errors.Add(new Error("Err102", "No user found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of User.");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new UserModelResponse(metadata, lstUsers);
        }
        public Response CreateNewUser(UserShortRegister user)
        {
            var errors = new List<Error>();
            int outPutResult = 0;
            var alreadyInsertedUser = _context.User.Where(x => x.Email == user.Email).Select(u => new UserModel
            {
                ID = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                CreatedDate = u.CreatedDate,
                ContactName = u.ContactName
            }).FirstOrDefault();
            if (alreadyInsertedUser != null && alreadyInsertedUser.Email!=string.Empty)
            {
                errors.Add(new Error("Err105", "User Already Added.."));
                return new ErrorResponse(new Metadata(errors.Any(), Guid.NewGuid().ToString(), "Response Contains User Details Of User"), errors);                
            }
            Matrimony.Data.Entities.User dbUser = new Data.Entities.User()
            {
                Password = user.Password,
                CreatedDate = DateTime.Now,
                Email = user.Email,
                ProfileCreatedForId = user.ProfileCreatedForId,
                PhoneNumber = user.PhoneNumber,
                ContactName = user.Email + "_" + user.PhoneNumber
            };
            

            try
            {
                _context.User.Add(dbUser);
                //_context.UserInfo.Add(dbUserInfo);
                outPutResult = _context.SaveChanges();
                if (outPutResult != 0)
                {
                    var newinsertedUserID = _context.User.FirstOrDefault(x => x.Email == user.Email).Id;
                    if(newinsertedUserID > 0)
                    {
                        Data.Entities.UserInfo dbUserInfo = new Data.Entities.UserInfo()
                        {
                            UserId = newinsertedUserID
                        };
                        _context.UserInfo.Add(dbUserInfo);
                        outPutResult = _context.SaveChanges();
                    }
                }
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
                var insertedUser = GetUserInformation(user.Email);
                return new UserModelResponse(metadata, insertedUser);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }
        private UserModel GetUserInformation(int id)
        {
            UserModel returnValue = new UserModel();
            returnValue = (from u in _context.User
                           join ui in _context.UserInfo on u.Id equals ui.UserId into user_basic
                           from ub in user_basic.DefaultIfEmpty()
                           select new UserModel
                           {
                               ID = u.Id,
                               Email = u.Email,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               MiddleNmae = u.MiddleNmae,
                               PhoneNumber = u.PhoneNumber,
                               ProfileCreatedForId = u.ProfileCreatedForId,
                               UserBasicInfo = new UserBasicInformation
                               {
                                   Id = ub.Id,
                                   UserId = ub.UserId,
                                   GenderId = ub.GenderId,
                                   Dob = ub.Dob,
                                   MaritalStatusId = ub.MaritalStatusId,
                                   Height = ub.Height,
                                   Weight = ub.Weight,
                                   BodyTypeId = ub.BodyTypeId,
                                   ComplexionId = ub.ComplexionId,
                                   IsDisability = ub.IsDisability,
                                   BloodGroupId = ub.BloodGroupId,
                                   ReligionId = ub.ReligionId,
                                   Caste = ub.Caste,
                                   MotherTongueId = ub.MotherTongueId,
                                   ComunityId = ub.ComunityId,
                                   Gothra = ub.Gothra,
                                   CountryId = ub.CountryId,
                                   CitizenshipId = ub.CitizenshipId,
                                   StateId = ub.StateId,
                                   CityId = ub.CityId,
                                   GrewUpIn = ub.GrewUpIn,
                                   Origin = ub.Origin,
                                   Pin = ub.Pin
                               },
                               UserImages = _context.UserImage.Where(i => i.UserId.Equals(id)).Select(u => new UserImage
                               {
                                   Id = u.Id,
                                   UserId = u.UserId,
                                   ImageString = "data:" + u.ContentType + ";base64," + GenericHelper.ResizeImage((byte[])u.Image, 0, 0, "") // ImageResizer((byte[])u.Image, width, height)
                               }).ToList()

                           }).FirstOrDefault();


           //var IQueryUsers = _context.User.Where(u => u.Id.Equals(id)).Select(u => new UserModel
           // {
           //     ID = u.Id,
           //     Email = u.Email,
           //     FirstName = u.FirstName,
           //     LastName = u.LastName,
           //     MiddleNmae = u.MiddleNmae,
           //     PhoneNumber = u.PhoneNumber,
           //     ProfileCreatedForId = u.ProfileCreatedForId
           // });
           // returnValue = IQueryUsers.FirstOrDefault();
           // returnValue.UserBasicInfo = _context.UserInfo.Where(x => x.UserId == returnValue.ID).Select(
           //     userinfo => new UserBasicInformation()
           //     {
           //         Id = userinfo.Id,
           //         UserId = userinfo.UserId,
           //         GenderId = userinfo.GenderId,
           //         Dob = userinfo.Dob,
           //         MaritalStatusId = userinfo.MaritalStatusId,
           //         Height = userinfo.Height,
           //         Weight = userinfo.Weight,
           //         BodyTypeId = userinfo.BodyTypeId,
           //         ComplexionId = userinfo.ComplexionId,
           //         IsDisability = userinfo.IsDisability,
           //         BloodGroupId = userinfo.BloodGroupId,
           //         ReligionId = userinfo.ReligionId,
           //         Caste = userinfo.Caste,
           //         MotherTongueId = userinfo.MotherTongueId,
           //         ComunityId = userinfo.ComunityId,
           //         Gothra = userinfo.Gothra,
           //         CountryId = userinfo.CountryId,
           //         CitizenshipId = userinfo.CitizenshipId,
           //         StateId = userinfo.StateId,
           //         CityId = userinfo.CityId,
           //         GrewUpIn = userinfo.GrewUpIn,
           //         Origin = userinfo.Origin,
           //         Pin = userinfo.Pin
           //     }).FirstOrDefault();

            return returnValue;
        }

        private UserModel GetUserInformation(string userEmail)
        {
            UserModel returnValue = new UserModel();
            returnValue = _context.User.Where(x => x.Email == userEmail).Select(u => new UserModel
            {
                ID = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                CreatedDate = u.CreatedDate,
                ContactName = u.ContactName,
                ProfileCreatedForId = u.ProfileCreatedForId
            }).FirstOrDefault();
            returnValue.UserBasicInfo = _context.UserInfo.Where(x => x.UserId == returnValue.ID).Select(
                userinfo => new UserBasicInformation()
                {
                    Id = userinfo.Id,
                    UserId = userinfo.UserId,
                    GenderId = userinfo.GenderId,
                    Dob = userinfo.Dob,
                    MaritalStatusId = userinfo.MaritalStatusId,
                    Height = userinfo.Height,
                    Weight = userinfo.Weight,
                    BodyTypeId = userinfo.BodyTypeId,
                    ComplexionId = userinfo.ComplexionId,
                    IsDisability = userinfo.IsDisability,
                    BloodGroupId = userinfo.BloodGroupId,
                    ReligionId = userinfo.ReligionId,
                    Caste = userinfo.Caste,
                    MotherTongueId = userinfo.MotherTongueId,
                    ComunityId = userinfo.ComunityId,
                    Gothra = userinfo.Gothra,
                    CountryId = userinfo.CountryId,
                    CitizenshipId = userinfo.CitizenshipId,
                    StateId = userinfo.StateId,
                    CityId = userinfo.CityId,
                    GrewUpIn = userinfo.GrewUpIn,
                    Origin = userinfo.Origin,
                    Pin = userinfo.Pin
                }).FirstOrDefault();

            return returnValue;
        }
        public Response GestUserList()
        {
            var errors = new List<Error>();
            var lstUsers = (from u in _context.User
                            join ui in _context.UserInfo on u.Id equals ui.UserId into user_basic
                            from ub in user_basic.DefaultIfEmpty()
                            join mLang in _context.MasterFieldValue on ub.MotherTongueId equals mLang.Id into language
                            from l in language.DefaultIfEmpty()
                            join mEdu in _context.MasterFieldValue on ub.HighestQualificationId equals mEdu.Id into highstEducation
                            from he in highstEducation.DefaultIfEmpty()
                            join mEduField in _context.MasterFieldValue on ub.HighestSpecializationId equals mEduField.Id into highstEducationField
                            from hef in highstEducationField.DefaultIfEmpty()
                            join mWork in _context.MasterFieldValue on ub.WorkDesignationId equals mWork.Id into workDesignation
                            from w in workDesignation.DefaultIfEmpty()
                            join ct in _context.Cities on ub.CityId equals ct.Id into city
                            from c in city.DefaultIfEmpty()
                            select new
                            {
                                Id = u.Id,
                                Name = string.Concat(u.FirstName, " ", u.MiddleNmae, " ", u.LastName),
                                Age = GenericHelper.CalculateAge(Convert.ToDateTime(ub.Dob)),
                                Height = ub.Height,
                                Education = string.Concat(he.Value ?? string.Empty, ", ", hef.Value ?? string.Empty),
                                City = c.Name ?? string.Empty,
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
                    case "UserEducationCareerModel":
                        UserEducationCareerModel userEducationsCareer = (UserEducationCareerModel)obj;
                        outPutResult = InsertUpdateUserEducationCareer(userEducationsCareer);
                        userId = userEducationsCareer.UserId;
                        break;
                    case "UserReligionCasteModel":
                        UserReligionCasteModel userReligion = (UserReligionCasteModel)obj;
                        outPutResult = InsertUpdateUserReligion(userReligion);
                        userId = userReligion.UserId;
                        break;
                    case "UserAboutModel":
                        UserAboutModel userAbout = (UserAboutModel)obj;
                        outPutResult = InsertUpdateUserAboutInfo(userAbout);
                        userId = userAbout.UserId;
                        break;
                    default:
                        // code block
                        break;
                }
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
                UserModel userModel = GetUserInformation(insertedUser.Id);

                return new UserModelResponse(metadata, userModel);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }

        public async Task<Response> SaveImage(List<UserImage> userImages)
        {
            int stat = 0;
            var errors = new List<Error>();
            try
            {
                stat = await SaveUserImage(userImages);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (stat == 0)
            {
                errors.Add(new Error("Err102", "Can not Add User.."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains Image Of User");
            if (!errors.Any())
            {               

                return new AnonymousResponse(metadata, stat);
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
                errors.Add(new Error("Err102", "No image found. Verify user entitlements."));
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
        private async Task<int> SaveUserImage(List<UserImage> userImgs)
        {
            int outPutResult = 0;
            userImgs.ForEach(img =>
            {
                Matrimony.Data.Entities.UserImage dbUserImage = new Data.Entities.UserImage()
                {
                    Id = img.Id,
                    UserId = img.UserId,
                    Image = img.Image,
                    ContentType = img.ContentType
                };
                try
                {
                    if (img.Id > 0)
                    {
                        _context.Update<Matrimony.Data.Entities.UserImage>(dbUserImage);
                    }
                    else
                    {
                        _context.UserImage.Add(dbUserImage);
                    }
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            outPutResult = await _context.SaveChangesAsync();
            return outPutResult;
        }
        private int InsertUpdateUserBasicInfo(UserBasicInformation userBasic)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserInfo uInfo = _context.UserInfo.Where(u => u.UserId.Equals(userBasic.UserId)).FirstOrDefault();
            if (uInfo == null)
            {
                uInfo = new Data.Entities.UserInfo();
                uInfo.Id = userBasic.Id;
            }            
            uInfo.UserId = userBasic.UserId;
            uInfo.GenderId = userBasic.GenderId;
            uInfo.Dob = userBasic.Dob;
            //uInfo.About = userBasic.About;
            uInfo.BloodGroupId = userBasic.BloodGroupId;
            uInfo.ComunityId = userBasic.ComunityId;
            uInfo.MaritalStatusId = userBasic.MaritalStatusId;
            uInfo.Height = userBasic.Height;
            uInfo.Weight = userBasic.Weight;
            uInfo.BodyTypeId = userBasic.BodyTypeId;
            uInfo.IsDisability = userBasic.IsDisability;
            uInfo.ReligionId = userBasic.ReligionId;
            uInfo.MotherTongueId = userBasic.MotherTongueId;
            uInfo.Gothra = userBasic.Gothra;
            //uInfo.IsIgnorCast = userBasic.IsIgnorCast;
            uInfo.ComplexionId = userBasic.ComplexionId;
            uInfo.Caste = userBasic.Caste;
            uInfo.CountryId = userBasic.CountryId;
            uInfo.CitizenshipId = userBasic.CitizenshipId;
            uInfo.StateId = userBasic.StateId;
            uInfo.CityId = userBasic.CityId;
            uInfo.GrewUpIn = userBasic.GrewUpIn;
            uInfo.Origin = userBasic.Origin;
            uInfo.Pin = userBasic.Pin;
            try
            {
                if (uInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserInfo>(uInfo);
                }
                else
                {
                    _context.UserInfo.Add(uInfo);
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
            Matrimony.Data.Entities.UserInfo uInfo = _context.UserInfo.Where(u => u.UserId.Equals(userFamily.UserId)).FirstOrDefault();
            if (uInfo == null)
            {
                uInfo = new Data.Entities.UserInfo();
                uInfo.Id = userFamily.Id;
            }
            uInfo.UserId = userFamily.UserId;
            uInfo.FatherStatusId = userFamily.FatherStatusId;
            uInfo.MotherStatusId = userFamily.MotherStatusId;
            uInfo.NativePlace = userFamily.NativePlace;
            uInfo.FamilyLocation = userFamily.FamilyLocation;
            uInfo.MarriedSiblingMale = userFamily.MarriedSiblingMale;
            uInfo.NotMarriedSiblingMale = userFamily.NotMarriedSiblingMale;
            uInfo.MarriedSiblingFemale = userFamily.MarriedSiblingFemale;
            uInfo.NotMarriedSiblingFemale = userFamily.NotMarriedSiblingFemale;
            uInfo.FamilyTypeId = userFamily.FamilyTypeId;
            uInfo.FamilyValuesId = userFamily.FamilyValuesId;
            uInfo.FamilyIncomeId = userFamily.FamilyIncomeId;
            try
            {
                if (uInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserInfo>(uInfo);
                }
                else
                {
                    _context.UserInfo.Add(uInfo);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        private int InsertUpdateUserEducationCareer(UserEducationCareerModel user_edu_car)
        {
            int outPutResult = 0;
            try
            {
                Matrimony.Data.Entities.UserInfo uInfo = _context.UserInfo.Where(u => u.UserId.Equals(user_edu_car.UserId)).FirstOrDefault();
                if (uInfo == null)
                {
                    uInfo = new Data.Entities.UserInfo();
                    uInfo.Id = user_edu_car.Id;
                }
                uInfo.UserId = user_edu_car.UserId;
                uInfo.HighestQualificationId = user_edu_car.HighestQualificationId;
                uInfo.HighestSpecializationId = user_edu_car.HighestSpecializationId;
                uInfo.SecondaryQualificationId = user_edu_car.SecondaryQualificationId;
                uInfo.SecondarySpecializationId = user_edu_car.SecondarySpecializationId;
                uInfo.Institution = user_edu_car.Institution;
                uInfo.University = user_edu_car.University;
                uInfo.WorkingSectorId = user_edu_car.WorkingSectorId;
                uInfo.WorkDesignationId = user_edu_car.WorkDesignationId;
                uInfo.EmployerId = user_edu_car.EmployerId;
                uInfo.AnualIncomeId = user_edu_car.AnualIncomeId;
                uInfo.IsDisplayIncome = user_edu_car.IsDisplayIncome;

                if (uInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserInfo>(uInfo);
                }
                else
                {
                    _context.UserInfo.Add(uInfo);
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
                        ContactName = user.FirstName + " " + user.MiddleNmae + " " + user.LastName,
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

        private int InsertUpdateUserReligion(UserReligionCasteModel user_rel)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserInfo uInfo = _context.UserInfo.Where(u => u.UserId.Equals(user_rel.UserId)).FirstOrDefault();
            if (uInfo == null)
            {
                uInfo = new Data.Entities.UserInfo();
                uInfo.Id = user_rel.Id;
            }
            try
            {
                uInfo.UserId = user_rel.UserId;
                uInfo.ReligionId = user_rel.ReligionId;
                uInfo.Gothra = user_rel.Gothra;
                uInfo.IsIgnorCast = user_rel.IsIgnorCast;
                uInfo.Caste = user_rel.Caste;

                if (uInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserInfo>(uInfo);
                }
                else
                {
                    _context.UserInfo.Add(uInfo);
                }

                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        private int InsertUpdateUserAboutInfo(UserAboutModel user_about)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserInfo uInfo = _context.UserInfo.Where(u => u.UserId.Equals(user_about.UserId)).FirstOrDefault();
            if (uInfo == null)
            {
                uInfo = new Data.Entities.UserInfo();
                uInfo.Id = user_about.Id;
            }
            try
            {
                uInfo.UserId = user_about.UserId;
                uInfo.About = user_about.About;

                if (uInfo.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserInfo>(uInfo);
                }
                else
                {
                    _context.UserInfo.Add(uInfo);
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
