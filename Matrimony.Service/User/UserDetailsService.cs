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
using Matrimony.Model.Common;
using Matrimony_Model.Common;
using static Matrimony.Helper.EnumManager;

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

        public Response LoginUser(UserShortRegister user)
        {
            var errors = new List<Error>();
            var alreadyInsertedUser = (from u in _context.User.Where(x => x.Email == user.Email && x.Password == user.Password)
                                       join ui in _context.UserInfo on u.Id equals ui.UserId
                                       join p in _context.UserPreferences on u.Id equals p.UserId into up
                                       from pref in up.DefaultIfEmpty()
                                       select new UserModel
                                       {
                                           ID = u.Id,
                                           Email = u.Email,
                                           FirstName = u.FirstName,
                                           LastName = u.LastName,
                                           PhoneNumber = u.PhoneNumber,
                                           CreatedDate = u.CreatedDate,
                                           ContactName = u.ContactName,
                                           genderId = ui.GenderId,
                                           UserPreference = _mapper.Map<UserPreferenceModel>(pref)
                                       }).FirstOrDefault();
            if (alreadyInsertedUser != null && alreadyInsertedUser.Email != string.Empty)
            {
                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains User Details Of User");
                if (!errors.Any())
                {
                    //var insertedUser = GetUserInformation(alreadyInsertedUser.ID);
                    return new UserModelResponse(metadata, alreadyInsertedUser);
                }
                else
                {
                    return new ErrorResponse(metadata, errors);
                }
            }
            else
            {
                errors.Add(new Error("Err105", "Login Failed invalid Credentials.."));
                return new ErrorResponse(new Metadata(errors.Any(), Guid.NewGuid().ToString(), "Response Contains User Details Of User"), errors);
            }
        }

        public Response CreateNewUser(UserShortRegister user)
        {
            var errors = new List<Error>();
            int outPutResult = 0;
            int alreadyInsertedUser = _context.User.Where(x => x.Email == user.Email).Count();
            if (alreadyInsertedUser > 0)
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
                ContactName = user.Email + "_" + user.PhoneNumber,
                PercentageComplete = UserCompletionPercentage.GetUserCompletionPercentage(UserCompletionPercentage.ShortRegistration)
            };


            try
            {
                _context.User.Add(dbUser);
                //_context.UserInfo.Add(dbUserInfo);
                outPutResult = _context.SaveChanges();
                var newinsertedUserID = dbUser.Id;
                if (outPutResult != 0)
                {
                    if (newinsertedUserID > 0)
                    {
                        int? genderID = null;
                        var metaData = from meta in _context.MasterTableMetadata
                                       join master in _context.MasterFieldValue on meta.Id equals master.MasterTableId
                                       where meta.TableName.Equals("ProfileCreatedFor")
                                       select new MasterDataModel()
                                       {
                                           Id = master.Id,
                                           Name = master.Value,
                                       };

                        if(metaData != null && metaData.Any(item=>item.Id == user.ProfileCreatedForId))
                        {
                            var gender = Helper.GenericHelper.Gender(metaData.FirstOrDefault(item => item.Id == user.ProfileCreatedForId).Name);
                            if (!string.IsNullOrEmpty(gender))
                            {
                                var allGenders= from meta in _context.MasterTableMetadata
                                               join master in _context.MasterFieldValue on meta.Id equals master.MasterTableId
                                               where meta.TableName.Equals("Gender")
                                               select new MasterDataModel()
                                               {
                                                   Id = master.Id,
                                                   Name = master.Value,
                                               };
                                genderID = allGenders.FirstOrDefault(item => item.Name.ToLower() == gender).Id;
                            }

                        }

                        Data.Entities.UserInfo dbUserInfo = new Data.Entities.UserInfo()
                        {
                            UserId = newinsertedUserID,
                            GenderId = genderID
                        };
                        _context.UserInfo.Add(dbUserInfo);
                        Data.Entities.UserProfileCompletion ProfCompletion = new Data.Entities.UserProfileCompletion()
                        {
                            UserId = newinsertedUserID
                        };
                         _context.UserProfileCompletion.Add(ProfCompletion);
                        outPutResult = _context.SaveChanges();
                        UpdateProfileCompletion(AvailableProfiles.ShortRegistration, ProfileCriteria.Mandatory, newinsertedUserID, true, false);
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
                           join usLife in _context.UserLifeStyle on u.Id equals usLife.UserId into user_Life
                           from userLife in user_Life.DefaultIfEmpty()
                           join up in _context.UserPreferences on u.Id equals up.UserId into user_pref
                           from preference in user_pref.DefaultIfEmpty()
                           //join uperc in _context.UserProfileCompletion on u.Id equals uperc.UserId into user_perc
                           //from userpercentage in user_perc.DefaultIfEmpty()
                           where u.Id.Equals(id)
                           select new UserModel
                           {
                               ID = u.Id,
                               Email = u.Email,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               MiddleNmae = u.MiddleNmae,
                               PhoneNumber = u.PhoneNumber,
                               ProfileCreatedForId = u.ProfileCreatedForId,
                               PercentageComplete = u.PercentageComplete,
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
                                   Dosh = ub.Dosh,
                                   Manglik = ub.Manglik,
                                   Horoscope = ub.Horoscope,
                                   BloodGroupId = ub.BloodGroupId,
                                   ReligionId = ub.ReligionId,
                                   CasteId = ub.CasteId,
                                   MotherTongueId = ub.MotherTongueId,
                                   ComunityId = ub.ComunityId,
                                   Gothra = ub.Gothra,
                                   CountryId = ub.CountryId,
                                   CitizenshipId = ub.CitizenshipId,
                                   StateId = ub.StateId,
                                   CityId = ub.CityId,
                                   GrewUpIn = ub.GrewUpIn,
                                   Origin = ub.Origin,
                                   Pin = ub.Pin,
                                   About = ub.About
                               },
                               //UserProfileCompletion = new UserPercentageComplete
                               //{
                               //    Id = userpercentage.Id,
                               //    UserId = userpercentage.UserId,
                               //    About = userpercentage.About == null ? false : (bool)userpercentage.About,
                               //    BasicDetails = userpercentage.BasicDetailsMandatory == null ? false : (bool)userpercentage.BasicDetailsMandatory,
                               //    Career = userpercentage.CareerMandatory == null ? false : (bool)userpercentage.CareerMandatory,
                               //    BasicRegister = userpercentage.ShortRegisterMandatory == null ? false : (bool)userpercentage.ShortRegisterMandatory,
                               //    Family = userpercentage.FamilyMandatory == null ? false : (bool)userpercentage.FamilyMandatory,
                               //    LifeStyle = userpercentage.LifeStyleMandatory == null ? false : (bool)userpercentage.LifeStyleMandatory,
                               //    PhotoUpload = userpercentage.PhotoUpload == null ? false : (bool)userpercentage.PhotoUpload,
                               //    Preference = userpercentage.PreferenceMandatory == null ? false : (bool)userpercentage.PreferenceMandatory,
                               //    Register = userpercentage.RegisterMandatory == null? false : (bool)userpercentage.RegisterMandatory,
                               //    Religion = userpercentage.ReligionMandatory == null? false : (bool)userpercentage.ReligionMandatory
                               //},
                               UserFamilyInfo = new UserFamilyInformationModel
                               {
                                   Id = ub.Id,
                                   UserId = ub.UserId,
                                   FatherStatusId = ub.FatherStatusId,
                                   MotherStatusId = ub.MotherStatusId,
                                   NativePlace = ub.NativePlace,
                                   FamilyLocation = ub.FamilyLocation,
                                   MarriedSiblingMale = ub.MarriedSiblingMale,
                                   NotMarriedSiblingMale = ub.NotMarriedSiblingMale,
                                   MarriedSiblingFemale = ub.MarriedSiblingFemale,
                                   NotMarriedSiblingFemale = ub.NotMarriedSiblingFemale,
                                   FamilyTypeId = ub.FamilyTypeId,
                                   FamilyValuesId = ub.FamilyValuesId,
                                   FamilyIncomeId = ub.FamilyIncomeId
                               },
                               UserCareerInfo = new UserEducationCareerModel
                               {
                                   Id = ub.Id,
                                   UserId = ub.UserId,
                                   HighestQualificationId = ub.HighestQualificationId,
                                   HighestSpecializationId = ub.HighestSpecializationId,
                                   SecondaryQualificationId = ub.SecondaryQualificationId,
                                   SecondarySpecializationId = ub.SecondarySpecializationId,
                                   Institution = ub.Institution,
                                   University = ub.University,
                                   WorkingSectorId = ub.WorkingSectorId,
                                   WorkDesignationId = ub.WorkDesignationId,
                                   EmployerId = ub.EmployerId,
                                   AnualIncomeId = ub.AnualIncomeId,
                                   IsDisplayIncome = ub.IsDisplayIncome
                               },
                               UserLifeStyle = new UserLifeStyleModel
                               {
                                   Id = userLife.Id,
                                   UserId = userLife.UserId,
                                   DietId = userLife.DietId,
                                   Hobies = userLife.Hobies,
                                   SmokingId = userLife.SmokingId,
                                   ChildrenChoiceId = userLife.ChildrenChoiceId,
                                   WeadingStyleId = userLife.WeadingStyleId,
                                   DrinkingId = userLife.DrinkingId,
                                   HouseLivingInId = userLife.HouseLivingInId,
                                   OwnCar = userLife.OwnCar,
                                   OwnPet = userLife.OwnPet,
                                   Interests = userLife.Interests,
                                   Musics = userLife.Musics,
                                   Books = userLife.Books,
                                   Movies = userLife.Movies,
                                   Fitness = userLife.Fitness,
                                   Cuisines = userLife.Cuisines
                               },
                               //UserLifeStyle = _context.UserLifeStyle.Where(i=>i.UserId.Equals(id)).FirstOrDefault().Select(userLife=> new UserLifeStyleModel
                               //{
                               //    Id = userLife.Id,
                               //    UserId = userLife.UserId,
                               //    DietId = userLife.DietId,
                               //    Hobies = userLife.Hobies,
                               //    SmokingId = userLife.SmokingId,
                               //    ChildrenChoiceId = userLife.ChildrenChoiceId,
                               //    WeadingStyleId = userLife.WeadingStyleId,
                               //    DrinkingId = userLife.DrinkingId,
                               //    HouseLivingInId = userLife.HouseLivingInId,
                               //    OwnCar = userLife.OwnCar,
                               //    OwnPet = userLife.OwnPet,
                               //    Interests = userLife.Interests,
                               //    Musics = userLife.Musics,
                               //    Books = userLife.Books,
                               //    Movies = userLife.Movies,
                               //    Fitness = userLife.Fitness,
                               //    Cuisines = userLife.Cuisines
                               //}),
                               UserImages = _context.UserImage.Where(i => i.UserId.Equals(id)).Select(u => new UserImage
                               {
                                   Id = u.Id,
                                   UserId = u.UserId,
                                   ImageString = "data:" + u.ContentType + ";base64," + GenericHelper.ResizeImage((byte[])u.Image, 0, 0, ""), // ImageResizer((byte[])u.Image, width, height)
                                   IsProfilePicture = u.IsProfilePicture
                               }).ToList(),
                               UserPreference = _mapper.Map<UserPreferenceModel>(preference)

                           }).FirstOrDefault();

            if(returnValue != null)
            {
                var userpercentage = _context.UserProfileCompletion.FirstOrDefault(x => x.UserId == returnValue.ID);
                if(userpercentage != null)
                {
                    returnValue.UserProfileCompletion = new UserPercentageComplete()
                    {
                        Id = userpercentage.Id,
                        UserId = userpercentage.UserId,
                        About = userpercentage.About == null ? false : (bool)userpercentage.About,
                        BasicDetails = userpercentage.BasicDetailsMandatory == null ? false : (bool)userpercentage.BasicDetailsMandatory,
                        Career = userpercentage.CareerMandatory == null ? false : (bool)userpercentage.CareerMandatory,
                        BasicRegister = userpercentage.ShortRegisterMandatory == null ? false : (bool)userpercentage.ShortRegisterMandatory,
                        Family = userpercentage.FamilyMandatory == null ? false : (bool)userpercentage.FamilyMandatory,
                        LifeStyle = userpercentage.LifeStyleMandatory == null ? false : (bool)userpercentage.LifeStyleMandatory,
                        PhotoUpload = userpercentage.PhotoUpload == null ? false : (bool)userpercentage.PhotoUpload,
                        Preference = userpercentage.PreferenceMandatory == null ? false : (bool)userpercentage.PreferenceMandatory,
                        Register = userpercentage.RegisterMandatory == null ? false : (bool)userpercentage.RegisterMandatory,
                        Religion = userpercentage.ReligionMandatory == null ? false : (bool)userpercentage.ReligionMandatory
                    };
                }
                
            }
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
                    Dosh = userinfo.Dosh,
                    Manglik = userinfo.Manglik,
                    Horoscope = userinfo.Horoscope,
                    BloodGroupId = userinfo.BloodGroupId,
                    ReligionId = userinfo.ReligionId,
                    CasteId = userinfo.CasteId,
                    MotherTongueId = userinfo.MotherTongueId,
                    ComunityId = userinfo.ComunityId,
                    Gothra = userinfo.Gothra,
                    CountryId = userinfo.CountryId,
                    CitizenshipId = userinfo.CitizenshipId,
                    StateId = userinfo.StateId,
                    CityId = userinfo.CityId,
                    GrewUpIn = userinfo.GrewUpIn,
                    Origin = userinfo.Origin,
                    Pin = userinfo.Pin,
                    About = userinfo.About
                }).FirstOrDefault();

            return returnValue;
        }
        public Response GestUserList(SearchCritriaModel searchCritria)
        {
            var errors = new List<Error>();
            Random rnd = new Random();
            var querySearch = (from u in _context.User.Where(u => !u.Id.Equals(searchCritria.UserId))
                               join ui in _context.UserInfo on u.Id equals ui.UserId into user_basic
                               from ub in user_basic.DefaultIfEmpty()
                               join uimg in _context.UserImage.Where(i => i.IsProfilePicture.Equals(true)) on u.Id equals uimg.UserId into user_image
                               from img in user_image.DefaultIfEmpty()
                               join s in _context.States on ub.StateId equals s.Id into user_state
                               from state in user_state.DefaultIfEmpty()
                               join ci in _context.Cities on ub.CityId equals ci.Id into user_city
                               from city in user_city.DefaultIfEmpty()
                               select new
                               {
                                   Id = u.Id,
                                   Name = string.Concat(u.FirstName ?? "", " ", u.MiddleNmae ?? "", " ", u.LastName ?? ""),
                                   Age = GenericHelper.CalculateAge(Convert.ToDateTime(ub.Dob)),
                                   Height = ub.Height ?? 0,
                                   CasteId = ub.CasteId,
                                   ub.HighestQualificationId,
                                   ub.HighestSpecializationId,
                                   ub.WorkDesignationId,
                                   State = state.Name,
                                   City = city.Name,
                                   Url = "",
                                   ImageString = !string.IsNullOrEmpty(img.ContentType) ? "data:" + img.ContentType + 
                                   ";base64," + GenericHelper.ResizeImage((byte[])img.Image, 300, 200, (searchCritria.UserId.Equals(0)) ? "Blur" : "Crop") : "",
                                   GenderId = ub.GenderId ?? 0,
                                   ReligionId = ub.ReligionId ?? 0,
                                   MotherTongueId = ub.MotherTongueId ?? 0
                               });
            if (!string.IsNullOrEmpty(searchCritria.Caste))
            {
                string[] castIds = searchCritria.Caste.Split(',');
                querySearch = querySearch.Where(u => castIds.Contains(u.CasteId.ToString()));
            }
            if (searchCritria.Gender > 0)
                querySearch = querySearch.Where(u => u.GenderId.Equals(searchCritria.Gender));
            if (!string.IsNullOrEmpty(searchCritria.Religion))
            {
                string[] religionIds = searchCritria.Religion.Split(',');
                querySearch = querySearch.Where(u => religionIds.Contains(u.ReligionId.ToString()));
            }
            if (!string.IsNullOrEmpty(searchCritria.MotherTongue))
            {
                string[] mtIds = searchCritria.MotherTongue.Split(',');
                querySearch = querySearch.Where(u => mtIds.Contains(u.MotherTongueId.ToString()));
            }

            var lstUsers = querySearch.ToList();
            if (searchCritria.AgeFrom > 0 && searchCritria.AgeTo > 0)
                lstUsers = lstUsers.Where(u => u.Age >= searchCritria.AgeFrom && u.Age <= searchCritria.AgeTo ).ToList();
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
            bool isUserOtherCareer = false;
            int otherCareerID = 0;
            bool mandatory = false, optional = false;
            try
            {
                //string objType = obj.GetType().Name;
                switch (type)
                {
                    case "UserRegister":
                        var userObject = (UserRegister)obj;
                        outPutResult = InsertUpdateUserInfo(userObject, out userId);
                        break;
                    case "UserBasicInformation":
                        UserBasicInformation userBasic = (UserBasicInformation)obj;
                        if (userBasic.GenderId.HasValue && userBasic.CitizenshipId.HasValue && userBasic.Dob.HasValue && userBasic.CountryId.HasValue && userBasic.StateId.HasValue && userBasic.CityId.HasValue && userBasic.MotherTongueId.HasValue)
                            mandatory = true;
                        if (userBasic.BloodGroupId.HasValue && userBasic.MaritalStatusId.HasValue && userBasic.IsDisability.HasValue && userBasic.ComplexionId.HasValue && userBasic.BodyTypeId.HasValue && userBasic.Height.HasValue && userBasic.Height!=0 && userBasic.Weight.HasValue)
                            optional = true;
                        else
                            optional = false;

                        //if (mandatory)
                        //{
                        //    UpdateProfileCompletion(AvailableProfiles.BasicDetails, ProfileCriteria.All, userBasic.UserId, mandatory, optional);
                        //}
                        //else
                        //{
                        //    UpdateProfileCompletion(AvailableProfiles.BasicDetails, ProfileCriteria.Optional, userBasic.UserId, false, optional);
                        //}
                        UpdateProfileCompletion(AvailableProfiles.BasicDetails, ProfileCriteria.All, userBasic.UserId, mandatory, optional);
                        outPutResult = InsertUpdateUserBasicInfo(userBasic);
                        userId = userBasic.UserId;
                        break;
                    case "UserFamilyInformationModel":
                        UserFamilyInformationModel userFamily = (UserFamilyInformationModel)obj;

                        if (userFamily.FatherStatusId.HasValue && userFamily.MotherStatusId.HasValue)
                            mandatory = true;
                        if (userFamily.MarriedSiblingFemale.HasValue && userFamily.NotMarriedSiblingFemale.HasValue &&
                            userFamily.MarriedSiblingMale.HasValue && userFamily.NotMarriedSiblingMale.HasValue 
                            && userFamily.FamilyTypeId.HasValue && userFamily.FamilyValuesId.HasValue && userFamily.FamilyLocation!=string.Empty
                            && userFamily.NativePlace !=string.Empty && userFamily.FamilyIncomeId.HasValue)
                            optional = true;
                        else
                            optional = false;

                        UpdateProfileCompletion(AvailableProfiles.FamilyDetails, ProfileCriteria.All, userFamily.UserId, mandatory, optional);

                        outPutResult = InsertUpdateUserFamilyInfo(userFamily);
                        userId = userFamily.UserId;
                        break;
                    case "UserImage":
                        UserImage userImage = (UserImage)obj;
                        if (userImage.ImageTitle != string.Empty)
                            mandatory = true;
                        else
                            mandatory = false;
                        UpdateProfileCompletion(AvailableProfiles.Image, ProfileCriteria.Mandatory, userImage.UserId, mandatory, optional);

                        outPutResult = InsertUpdateUserImage(userImage);
                        userId = userImage.UserId;
                        break;
                    case "UserLifeStyleModel":
                        UserLifeStyleModel userlifeStyle = (UserLifeStyleModel)obj;

                        if (userlifeStyle.DietId.HasValue
                            && userlifeStyle.HouseLivingInId.HasValue
                            && userlifeStyle.SmokingId.HasValue && userlifeStyle.WeadingStyleId.HasValue &&
                            userlifeStyle.Musics !=string.Empty && userlifeStyle.Movies!=string.Empty &&
                            userlifeStyle.Interests !=string.Empty && userlifeStyle.Hobies!=string.Empty 
                            && userlifeStyle.Cuisines!=string.Empty)
                            mandatory = true;
                        else
                            mandatory = false;

                        if (userlifeStyle.ChildrenChoiceId.HasValue && userlifeStyle.DrinkingId.HasValue &&
                            userlifeStyle.OwnCar.HasValue && userlifeStyle.OwnPet.HasValue && userlifeStyle.Fitness != string.Empty
                            && userlifeStyle.Books != string.Empty)
                            optional = true;
                        else
                            optional = false;

                        UpdateProfileCompletion(AvailableProfiles.LifeStyle, ProfileCriteria.All, userlifeStyle.UserId, mandatory, optional);
                        
                        outPutResult = InsertUpdateUserLifeStyle(userlifeStyle);
                        userId = userlifeStyle.UserId;
                        break;
                    case "UserEducationCareerModel":
                        UserEducationCareerModel userEducationsCareer = (UserEducationCareerModel)obj;
                        if (!string.IsNullOrEmpty(userEducationsCareer.OtherEmployer))
                        {
                            isUserOtherCareer = true;
                            otherCareerID = userEducationsCareer.EmployerId != null ? (int)userEducationsCareer.EmployerId : 0;
                        }
                        if (userEducationsCareer.HighestQualificationId.HasValue && userEducationsCareer.HighestSpecializationId.HasValue
                            && userEducationsCareer.WorkingSectorId.HasValue && userEducationsCareer.WorkDesignationId.HasValue
                            && userEducationsCareer.AnualIncomeId.HasValue)
                            mandatory = true;
                        if (userEducationsCareer.EmployerId.HasValue || userEducationsCareer.OtherEmployer != string.Empty)
                            optional = true;
                        else
                            optional = false;

                        UpdateProfileCompletion(AvailableProfiles.CareerEducation, ProfileCriteria.All, userEducationsCareer.UserId, mandatory, optional);

                        outPutResult = InsertUpdateUserEducationCareer(userEducationsCareer);
                        userId = userEducationsCareer.UserId;
                        break;
                    case "UserReligionCasteModel":
                        UserReligionCasteModel userReligion = (UserReligionCasteModel)obj;

                        if (userReligion.ReligionId.HasValue && userReligion.CasteId.HasValue)
                            mandatory = true;
                        if (userReligion.Dosh.HasValue && userReligion.Manglik.HasValue &&
                            userReligion.Horoscope.HasValue && userReligion.Gothra != string.Empty)
                            optional = true;
                        else
                            optional = false;

                        UpdateProfileCompletion(AvailableProfiles.ReligionCaste, ProfileCriteria.All, userReligion.UserId, mandatory, optional);

                        outPutResult = InsertUpdateUserReligion(userReligion);
                        userId = userReligion.UserId;
                        break;
                    case "UserAboutModel":
                        UserAboutModel userAbout = (UserAboutModel)obj;

                        if (userAbout.About != string.Empty )
                            optional = true;
                        else
                            optional = false;
                           UpdateProfileCompletion(AvailableProfiles.About, ProfileCriteria.Optional, userAbout.UserId, false, optional);

                        outPutResult = InsertUpdateUserAboutInfo(userAbout);
                        userId = userAbout.UserId;
                        break;
                    case "UserPreferenceModel":
                        UserPreferenceModel userPref = (UserPreferenceModel)obj;


                        if (userPref.AgeFrom.HasValue && userPref.AgeTo.HasValue
                            && userPref.AnnualIncome.HasValue && userPref.Caste != string.Empty &&
                            userPref.MotherTongue != string.Empty && 
                            userPref.Occupation != string.Empty && userPref.Religion != string.Empty )
                            mandatory = true;
                        else
                            mandatory = false;
                        if (userPref.City != string.Empty && userPref.Country != string.Empty
                            && userPref.Dosh.HasValue && userPref.HeightFrom.HasValue && userPref.HeightTo.HasValue
                            && userPref.HighestQualification != string.Empty && userPref.Manglik.HasValue &&
                            userPref.Specialization != string.Empty && userPref.State != string.Empty)
                            optional = true;
                        else
                            optional = false;
                            
                        UpdateProfileCompletion(AvailableProfiles.Preference, ProfileCriteria.All, userPref.UserId, mandatory, optional);

                        outPutResult = InsertUpdateUserPreference(userPref);
                        userId = userPref.UserId;
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
                //var insertedUser = _context.User.Where(x => x.Id == userId).FirstOrDefault();
                UserModel userModel = GetUserInformation(userId);
                if (isUserOtherCareer)
                {
                    userModel.UserCareerInfo.EmployerId = otherCareerID == 0 ? userModel.UserCareerInfo.EmployerId : otherCareerID;
                }
                return new UserModelResponse(metadata, userModel);
            }
            else
            {
                return new ErrorResponse(metadata, errors);
            }
        }

        public async Task<Response> SaveImage(UserImagesUploadModel userImages, int userId)
        {
            int stat = 0;
            var errors = new List<Error>();
            try
            {
                stat = await SaveUserImage(userImages, userId);
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

        public Response GetUserPreferences(int userId)
        {
            var errors = new List<Error>();
            UserPreferenceModel preference = new UserPreferenceModel();
            try 
            {
                var IQueryPref = _context.UserPreferences.Where(p => p.UserId.Equals(userId));
                preference = _mapper.Map<UserPreferenceModel>(IQueryPref.FirstOrDefault());
            }
            catch(Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (preference == null)
            {
                errors.Add(new Error("Err102", "No data found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains preference Of User");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new UserPreferenceResponse(metadata, preference);
        }
        
        private static bool GetProfileCompletionPercentage(bool? incomingValue)
        {
            bool returnValue = false;
            if(incomingValue!=null)
            {
                returnValue = (bool)incomingValue;
            }
            return returnValue;
        }
        private int InsertUpdateUserLifeStyle(UserLifeStyleModel userLife)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserLifeStyle dbUserLifeStyle = _context.UserLifeStyle.Where(x => x.UserId == userLife.UserId).FirstOrDefault();
            if (dbUserLifeStyle == null)
            {
                dbUserLifeStyle = new Data.Entities.UserLifeStyle();
                dbUserLifeStyle.Id = userLife.Id;
            }
            dbUserLifeStyle.UserId = userLife.UserId;
            dbUserLifeStyle.DietId = userLife.DietId;
            dbUserLifeStyle.Hobies = userLife.Hobies;
            dbUserLifeStyle.SmokingId = userLife.SmokingId;
            dbUserLifeStyle.ChildrenChoiceId = userLife.ChildrenChoiceId;
            dbUserLifeStyle.WeadingStyleId = userLife.WeadingStyleId;
            dbUserLifeStyle.DrinkingId = userLife.DrinkingId;
            dbUserLifeStyle.HouseLivingInId = userLife.HouseLivingInId;
            dbUserLifeStyle.OwnCar = userLife.OwnCar;
            dbUserLifeStyle.OwnPet = userLife.OwnPet;
            dbUserLifeStyle.Interests = userLife.Interests;
            dbUserLifeStyle.Musics = userLife.Musics;
            dbUserLifeStyle.Books = userLife.Books;
            dbUserLifeStyle.Movies = userLife.Movies;
            dbUserLifeStyle.Fitness = userLife.Fitness;
            dbUserLifeStyle.Cuisines = userLife.Cuisines;
            try
            {
                if (dbUserLifeStyle.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserLifeStyle>(dbUserLifeStyle);
                }
                else
                {
                    _context.UserLifeStyle.Add(dbUserLifeStyle);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
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
        private async Task<int> SaveUserImage(UserImagesUploadModel userImgModel, int userId)
        {
            int outPutResult = 0;
            if (userImgModel.imageIDsToDelete != null && userImgModel.imageIDsToDelete.Count > 0)
            {
                try
                {
                    var imageToDelete = _context.UserImage.Where(u => u.UserId.Equals(userId) && userImgModel.imageIDsToDelete.Contains(u.Id)).ToList();
                    imageToDelete.ForEach(i =>
                    {
                        _context.UserImage.Remove(i);
                    });
                    var imageSetAsProfPic = _context.UserImage.Where(u => u.UserId.Equals(userId) && u.IsProfilePicture.Equals(true));
                    int count = imageSetAsProfPic.Count();
                    if(count == 0)
                    {
                        UpdateProfileCompletion(AvailableProfiles.Image, ProfileCriteria.Mandatory, userId, false, false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (userImgModel.profilePictureId > 0)
            {
                try
                {
                    var imageSetAsProfPic = _context.UserImage.Where(u => u.UserId.Equals(userId) && u.IsProfilePicture.Equals(true)).FirstOrDefault();
                    if (imageSetAsProfPic != null)
                    {
                        imageSetAsProfPic.IsProfilePicture = false;
                        _context.Update<Matrimony.Data.Entities.UserImage>(imageSetAsProfPic);
                    }
                    var imageToSetProfPic = _context.UserImage.Where(u => u.UserId.Equals(userId) && u.Id.Equals(userImgModel.profilePictureId)).FirstOrDefault();
                    if (imageToSetProfPic != null)
                    {
                        imageToSetProfPic.IsProfilePicture = true;
                        _context.Update<Matrimony.Data.Entities.UserImage>(imageToSetProfPic);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (userImgModel.images != null && userImgModel.images.Count > 0)
            {
                var userImgs = userImgModel.images;
                var imageSetAsProfPic = _context.UserImage.Where(u => u.UserId.Equals(userImgs[0].UserId) && u.IsProfilePicture.Equals(true));
                int count = imageSetAsProfPic.Count();
                if (userImgs.Where(i => i.IsProfilePicture.Equals(true)).Count() > 0)
                {
                    if (count > 0)
                    {
                        var imagetoUpdate = imageSetAsProfPic.FirstOrDefault();
                        imagetoUpdate.IsProfilePicture = false;
                        _context.Update<Matrimony.Data.Entities.UserImage>(imagetoUpdate);
                    }
                }
                userImgs.ForEach(img =>
                {
                    string base64String = img.ImageString.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64String);


                    Matrimony.Data.Entities.UserImage dbUserImage = new Data.Entities.UserImage()
                    {
                        Id = img.Id,
                        UserId = img.UserId,
                        Image = imageBytes,
                        ContentType = img.ContentType,
                        IsProfilePicture = img.IsProfilePicture
                    };
                    if (count == 0)
                    {
                        dbUserImage.IsProfilePicture = true;
                        UpdateProfileCompletion(AvailableProfiles.Image, ProfileCriteria.Mandatory, userId, true, false);
                        count = 1;
                    }
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
            }
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
            //uInfo.ReligionId = userBasic.ReligionId;
            uInfo.MotherTongueId = userBasic.MotherTongueId;
            //uInfo.Gothra = userBasic.Gothra;
            //uInfo.IsIgnorCast = userBasic.IsIgnorCast;
            uInfo.ComplexionId = userBasic.ComplexionId;
            //uInfo.Caste = userBasic.Caste;
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
                    uInfo.UserId = user_edu_car.UserId;
                }
                if (!string.IsNullOrEmpty(user_edu_car.OtherEmployer))
                {
                    var queryData = from v in _context.MasterFieldValue
                                    join m in _context.MasterTableMetadata on v.MasterTableId equals m.Id
                                    where m.TableName == "Employer"
                                    select new MasterDataModel
                                    {
                                        MasterTableId = m.Id,
                                        Name = v.Value,
                                    };
                    if (queryData != null && queryData.Count() > 0 && queryData.FirstOrDefault(item => item.Name == user_edu_car.OtherEmployer) == null)
                    {
                        var masterTableId = queryData.Select(x => x.MasterTableId).FirstOrDefault();
                        _context.MasterFieldValue.Add(new Data.Entities.MasterFieldValue() { Value = user_edu_car.OtherEmployer, MasterTableId = masterTableId });
                        outPutResult = _context.SaveChanges();
                        if (outPutResult != 0)
                        {
                            user_edu_car.EmployerId = _context.MasterFieldValue.FirstOrDefault(item => item.MasterTableId == masterTableId &&
                            item.Value == user_edu_car.OtherEmployer).Id;
                        }
                    }
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

        //private bool IsValueChangeProfileProgress(bool? FieldValue, bool changeValue)
        //{

        //}

        private ProfileProgress GetProfileProgress(bool? FieldValue, bool changeValue, ProfileCriteria profileCriteria)
        {
            ProfileProgress returnValue = ProfileProgress.NoChange;

            if(profileCriteria == ProfileCriteria.Mandatory)
            {
                if ((FieldValue.HasValue && FieldValue.Value == changeValue) || (!FieldValue.HasValue && !changeValue))
                    returnValue = ProfileProgress.NoChange;
                else
                {
                    returnValue = changeValue ? ProfileProgress.Increase : ProfileProgress.Decrease;
                }
                    
            }
            else if(profileCriteria == ProfileCriteria.Optional)
            {
                if ((FieldValue.HasValue && FieldValue.Value == changeValue) || (!FieldValue.HasValue && !changeValue))
                    returnValue = ProfileProgress.NoChange;
                else
                {
                    returnValue = changeValue ? ProfileProgress.Increase : ProfileProgress.Decrease;
                }
            }
            return returnValue;
        }

        private void SetProfileProgress(int currentProgressPercent, string progressKey, int userId)
        {
            ProfileProgress returnValue = ProfileProgress.NoChange;
            int progressPercent = 0;
            int valueToReduce = UserCompletionPercentage.GetUserCompletionPercentage(progressKey);
            if (currentProgressPercent < 0)
            {
                progressPercent = valueToReduce;
                returnValue = ProfileProgress.Decrease;
            }
            else if (currentProgressPercent == 0)
            {
                returnValue = ProfileProgress.NoChange;
            }
            else
            {
                progressPercent = currentProgressPercent;
                returnValue = ProfileProgress.Increase;
            }
            UpdateUserCompletion(progressPercent, returnValue, userId);
        }

        private int GetProfilePercentageForEachGroup(string progressKey, int currentProgress, ProfileProgress profileProgress)
        {
            int returnValue = 0;

            if (profileProgress == ProfileProgress.Increase)
            {
                returnValue = currentProgress + UserCompletionPercentage.GetUserCompletionPercentage(progressKey);
            }else if(profileProgress == ProfileProgress.Decrease)
            {
                returnValue = currentProgress - UserCompletionPercentage.GetUserCompletionPercentage(progressKey);
            }
            else
            {
                returnValue = currentProgress;
            }

            return returnValue;
        }

        private void SetCurrentProfileProgress(ProfileCriteria currentCriteria,string mandatoryPercent,string optionalPercent,
            bool?mandatoryFieldCurrentValue, 
            bool?optionalFieldCurrentValue,bool mandatory, bool optional, int userId)
        {
            string percentToDecrease = optionalPercent;
            int progressPercent = 0;
            if(currentCriteria == ProfileCriteria.All || currentCriteria == ProfileCriteria.Mandatory)
            {
                //mandatory Check
                progressPercent = GetProfilePercentageForEachGroup(mandatoryPercent, progressPercent,
                            GetProfileProgress(mandatoryFieldCurrentValue, mandatory, ProfileCriteria.Mandatory));
                percentToDecrease = mandatoryPercent;
            }
            if(currentCriteria == ProfileCriteria.All || currentCriteria == ProfileCriteria.Optional)
            {
                //optional Check
                progressPercent = GetProfilePercentageForEachGroup(optionalPercent, progressPercent,
                    GetProfileProgress(optionalFieldCurrentValue, optional, ProfileCriteria.Optional));
                percentToDecrease = optionalPercent;
            }
            SetProfileProgress(progressPercent, percentToDecrease, userId);
        }
        private int UpdateProfileCompletion(AvailableProfiles allprofiles, ProfileCriteria profileCriteria, int userId, bool mandatory = false, bool optional = false)
        {
            int returnValue = 0;

            var currentProileCompletion = _context.UserProfileCompletion.FirstOrDefault(item => item.UserId == userId);
            int progressPercent = 0;
            bool changeValue = false;
            if(currentProileCompletion!=null && currentProileCompletion.UserId > 0)
            {
                switch (allprofiles)
                {
                    case AvailableProfiles.ShortRegistration:
                        currentProileCompletion.ShortRegisterMandatory = mandatory;
                        changeValue = true;
                        break;
                    case AvailableProfiles.Registration:
                        if(!currentProileCompletion.RegisterMandatory.HasValue || currentProileCompletion.RegisterMandatory.Value !=mandatory)
                        {
                            changeValue = true;
                            currentProileCompletion.RegisterMandatory = mandatory;
                        }
                        break;
                    case AvailableProfiles.Image:

                        //MandatoryCheck
                        progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.Image, progressPercent,
                            GetProfileProgress(currentProileCompletion.PhotoUpload, mandatory, ProfileCriteria.Mandatory));

                        SetProfileProgress(progressPercent, UserCompletionPercentage.Image, userId);

                        currentProileCompletion.PhotoUpload = mandatory;
                        changeValue = true;
                        break;
                    case AvailableProfiles.BasicDetails:

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.BasicDetailsMandatory,
                            UserCompletionPercentage.BasicDetailsOptional, currentProileCompletion.BasicDetailsMandatory,
                            currentProileCompletion.BasicDetailsOptional, mandatory, optional, userId);

                        currentProileCompletion.BasicDetailsMandatory = mandatory;
                        currentProileCompletion.BasicDetailsOptional = optional;
                        changeValue = true;

                        break;
                    case AvailableProfiles.ReligionCaste:

                        ////MandatoryCheck
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.ReligionCasteMandatory, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.ReligionMandatory, mandatory, ProfileCriteria.Mandatory));

                        ////optional Check
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.ReligionCasteOptional, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.ReligionOptional, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.ReligionCasteOptional, userId);

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.ReligionCasteMandatory,
                            UserCompletionPercentage.ReligionCasteOptional, currentProileCompletion.ReligionMandatory,
                            currentProileCompletion.ReligionOptional, mandatory, optional, userId);

                        currentProileCompletion.ReligionMandatory = mandatory;
                        currentProileCompletion.ReligionOptional = optional;
                        changeValue = true;
                        break;
                    case AvailableProfiles.CareerEducation:

                        ////MandatoryCheck
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.CareerEducationMandatory, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.CareerMandatory, mandatory, ProfileCriteria.Mandatory));

                        ////optional Check
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.CareerEducationOptional, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.CareerOptional, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.CareerEducationOptional, userId);

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.CareerEducationMandatory,
                            UserCompletionPercentage.CareerEducationOptional, currentProileCompletion.CareerMandatory,
                            currentProileCompletion.CareerOptional, mandatory, optional, userId);


                        currentProileCompletion.CareerMandatory = mandatory;
                        currentProileCompletion.CareerOptional = optional;
                        changeValue = true;
                        break;
                    case AvailableProfiles.FamilyDetails:

                        ////MandatoryCheck
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.FamilyDetailsMandatory, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.FamilyMandatory, mandatory, ProfileCriteria.Mandatory));

                        ////optional Check
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.FamilyDetailsOptional, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.FamilyOptional, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.FamilyDetailsOptional, userId);

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.FamilyDetailsMandatory,
                            UserCompletionPercentage.FamilyDetailsOptional, currentProileCompletion.FamilyMandatory,
                            currentProileCompletion.FamilyOptional, mandatory, optional, userId);

                        currentProileCompletion.FamilyMandatory = mandatory;
                        currentProileCompletion.FamilyOptional = optional;
                        changeValue = true;
                        break;
                    case AvailableProfiles.About:
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.About, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.About, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.About, userId);

                        SetCurrentProfileProgress(ProfileCriteria.Optional, UserCompletionPercentage.About,
                            UserCompletionPercentage.About, currentProileCompletion.About,
                            false, mandatory, optional, userId);

                        currentProileCompletion.About = optional;
                        changeValue = true;
                        break;
                    case AvailableProfiles.LifeStyle:

                        ////MandatoryCheck
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.LifeStyleMandatory, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.LifeStyleMandatory, mandatory, ProfileCriteria.Mandatory));

                        ////optional Check
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.LifeStyleOptional, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.LifeStyleOptional, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.LifeStyleOptional, userId);

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.LifeStyleMandatory,
                            UserCompletionPercentage.LifeStyleOptional, currentProileCompletion.LifeStyleMandatory,
                            currentProileCompletion.LifeStyleOptional, mandatory, optional, userId);

                        currentProileCompletion.LifeStyleMandatory = mandatory;
                        currentProileCompletion.LifeStyleOptional = optional;
                        changeValue = true;
                        break;
                    case AvailableProfiles.Preference:

                        ////MandatoryCheck
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.PreferenceMandatory, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.PreferenceMandatory, mandatory, ProfileCriteria.Mandatory));

                        ////optional Check
                        //progressPercent = GetProfilePercentageForEachGroup(UserCompletionPercentage.PreferenceOptional, progressPercent,
                        //    GetProfileProgress(currentProileCompletion.PreferenceOptional, optional, ProfileCriteria.Optional));

                        //SetProfileProgress(progressPercent, UserCompletionPercentage.PreferenceOptional, userId);

                        SetCurrentProfileProgress(ProfileCriteria.All, UserCompletionPercentage.PreferenceMandatory,
                            UserCompletionPercentage.PreferenceOptional, currentProileCompletion.PreferenceMandatory,
                            currentProileCompletion.PreferenceOptional, mandatory, optional, userId);

                        currentProileCompletion.PreferenceMandatory = mandatory;
                        currentProileCompletion.PreferenceOptional = optional;
                        changeValue = true;
                        break;
                    default:
                        break;
                }
                if (changeValue)
                {
                    _context.Update<Data.Entities.UserProfileCompletion>(currentProileCompletion);
                }
                
                //returnValue = _context.SaveChanges();
            }
            return returnValue;
        }
        private int UpdateUserCompletion(int completionPercentage, ProfileProgress profileProgress, int userID)
        {
            bool updatepercent = false;
            int returnValue = 0;
            int percentage = 0;
            var currentProfile = _context.User.Where(item => item.Id == userID).FirstOrDefault();
            
            if (currentProfile != null)
            {
                var currentProgress = currentProfile.PercentageComplete;
                if (currentProgress.HasValue && int.TryParse(currentProgress.ToString(), out percentage))
                {
                    if (profileProgress == ProfileProgress.Increase)
                    {
                        currentProfile.PercentageComplete = percentage + completionPercentage;
                        updatepercent = true;
                    }
                    else if(profileProgress == ProfileProgress.Decrease)
                    {
                        currentProfile.PercentageComplete = percentage - completionPercentage;
                        if(percentage - completionPercentage < 10)
                        {
                            currentProfile.PercentageComplete = 10;
                        }
                        updatepercent = true;
                    }
                    else
                    {
                        updatepercent = false;
                    }   
                }
                else
                {
                    currentProfile.PercentageComplete = completionPercentage;
                    updatepercent = true;
                }

                if (updatepercent)
                {
                    _context.Update<Data.Entities.User>(currentProfile);
                    returnValue = _context.SaveChanges();
                }
            }
            return returnValue;
        }
        private int InsertUpdateUserInfo(UserRegister user, out int userId)
        {
            int outPutResult = 0;
            int percentage = 0;
            Matrimony.Data.Entities.User dbUser = null;
            bool isProfileUpdate = false;
            try
            {
                if (user.ID > 0)
                {
                    dbUser = _context.User.FirstOrDefault(u => u.Id == user.ID);
                    if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                    {
                        if (dbUser.PercentageComplete.HasValue)
                        {
                            percentage = (int)dbUser.PercentageComplete;
                            if (!_context.UserProfileCompletion.FirstOrDefault(x => x.UserId == user.ID).RegisterMandatory.HasValue)
                            {
                                percentage = percentage + 10;
                                isProfileUpdate = true;
                            }
                                
                        }
                    }
                    dbUser.Id = user.ID;
                    dbUser.FirstName = user.FirstName;
                    dbUser.MiddleNmae = user.MiddleNmae;
                    dbUser.LastName = user.LastName;
                    dbUser.Email = user.Email;
                    dbUser.PhoneNumber = user.PhoneNumber;
                    dbUser.ProfileCreatedForId = user.ProfileCreatedForId;
                    dbUser.ContactName = user.FirstName + " " + user.MiddleNmae + " " + user.LastName;
                    dbUser.UpdatedDate = DateTime.Now;
                    dbUser.PercentageComplete = percentage;
                    //dbUser = new Data.Entities.User
                    //{
                    //    Id = user.ID,
                    //    FirstName = user.FirstName,
                    //    MiddleNmae = user.MiddleNmae,
                    //    LastName = user.LastName,
                    //    Email = user.Email,
                    //    PhoneNumber = user.PhoneNumber,
                    //    ProfileCreatedForId = user.ProfileCreatedForId,
                    //    ContactName = user.FirstName + " " + user.MiddleNmae + " " + user.LastName,
                    //    UpdatedDate = DateTime.Now,
                    //    PercentageComplete = percentage
                    //};
                    //_context.Entry<Data.Entities.User>(dbUser).State = EntityState.Detached;
                    _context.Update<Matrimony.Data.Entities.User>(dbUser);

                    //_context.Entry(dbUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //_context.Entry(dbUser).Property(x => x.CreatedDate).IsModified = false;
                    //_context.Entry(dbUser).Property(x => x.Password).IsModified = false;
                    if (isProfileUpdate)
                        UpdateProfileCompletion(AvailableProfiles.Registration, ProfileCriteria.Mandatory, user.ID, true, false);
                    outPutResult = _context.SaveChanges();
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
                        ContactName = user.ContactName,
                        PercentageComplete = 10
                    };
                    _context.User.Add(dbUser);
                    outPutResult = _context.SaveChanges();
                }

                

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
                uInfo.UserId = user_rel.Id;
            }
            try
            {
                //uInfo.UserId = user_rel.UserId;
                uInfo.ReligionId = user_rel.ReligionId;
                uInfo.Gothra = user_rel.Gothra;
                uInfo.IsIgnorCast = user_rel.IsIgnorCast;
                uInfo.Dosh = user_rel.Dosh;
                uInfo.CasteId = user_rel.CasteId;
                uInfo.Manglik = user_rel.Manglik;
                uInfo.Horoscope = user_rel.Horoscope;

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
                uInfo.UserId = user_about.Id;
            }
            try
            {
                //uInfo.UserId = user_about.UserId;
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

        private int InsertUpdateUserPreference(UserPreferenceModel userPref)
        {
            int outPutResult = 0;
            Matrimony.Data.Entities.UserPreferences uPreference = _mapper.Map<Matrimony.Data.Entities.UserPreferences>(userPref);
            //Matrimony.Data.Entities.UserPreferences uPreference = _context.UserPreferences.Where(u => u.UserId.Equals(userPref.UserId)).FirstOrDefault();
            //if (uPreference == null)
            //{
            //    uPreference = new Data.Entities.UserPreferences();
            //    uPreference.Id = userPref.Id;
            //}
            //uPreference.UserId = userPref.UserId;
            //uPreference.AgeFrom = userPref.AgeFrom;
            //uPreference.AgeTo = userPref.AgeTo;
            //uPreference.MaritialStatus = userPref.MaritialStatus;
            //uPreference.Country = userPref.Country;
            //uPreference.Citizenship = userPref.Citizenship;
            //uPreference.State = userPref.State;
            //uPreference.City = userPref.City;
            //uPreference.Religion = userPref.Religion;
            //uPreference.MotherTongue = userPref.MotherTongue;
            //uPreference.Caste = userPref.Caste;
            //uPreference.Subcaste = userPref.Subcaste;
            //uPreference.Gothram = userPref.Gothram;
            //uPreference.Dosh = userPref.Dosh;
            //uPreference.Manglik = userPref.Manglik;
            //uPreference.Horoscope = userPref.Horoscope;
            //uPreference.HighestQualification = userPref.HighestQualification;
            //uPreference.Working = userPref.Working;
            //uPreference.Occupation = userPref.Occupation;
            //uPreference.Specialization = userPref.Specialization;
            //uPreference.AnnualIncome = userPref.AnnualIncome;
            //uPreference.IsAccepted = userPref.IsAccepted;
            try
            {
                if (uPreference.Id > 0)
                {
                    _context.Update<Matrimony.Data.Entities.UserPreferences>(uPreference);
                }
                else
                {
                    _context.UserPreferences.Add(uPreference);
                }
                outPutResult = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outPutResult;
        }
        private IQueryable<Matrimony.Data.Entities.UserImage> GetRandomImage(int userId)
        {
            return _context.UserImage.Where(ui => ui.UserId.Equals(userId)).OrderBy(x => Guid.NewGuid()).Take(1);
        }

    }
}
