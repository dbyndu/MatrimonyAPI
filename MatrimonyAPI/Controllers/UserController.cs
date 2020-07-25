using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrimonyAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Model.User;
using Microsoft.Extensions.Options;
using MatrimonyAPI.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MatrimonyAPI.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using MatrimonyAPI.Handler;
using Matrimony.Model.Common;

namespace MatrimonyAPI.Controllers
{
    //[Authorize]
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDetailsService _userService;
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthenticationHelper _helper;
        private IConfiguration _config;
        private readonly IImageHandler _imageHandler;
        public UserController(IConfiguration config,ILogger<UserController> logger, IUserDetailsService userService, IOptions<JwtAuthentication> jwtAuthentication, IHttpContextAccessor httpContextAccessor,
            IImageHandler imageHandler)
        {
            _config = config;
            _logger = logger;
            _userService = userService;
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
            _httpContextAccessor = httpContextAccessor;
            _helper = new AuthenticationHelper();
            _imageHandler = imageHandler;
        }

        [HttpGet]
        //public ActionResult Register()
        //{
        //    //APIResponse aPIResponse = new APIResponse("Created", new HttpRequest().HttpContext);
        //    return Ok("Created");
        //}

        [HttpGet]
        //[Authorize]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetUser(int blabla)
        {
            return Ok(_userService.GetOneUserDetails("Srijit"));
        }

        [HttpGet]
        [Authorize]
        [Route("{GetOneUserDetails}")]
        [QueryStringConstraint("UserID",true)]
        [QueryStringConstraint("lob", false)]
        public ActionResult GetOneUserDetails(string UserID)
        {
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, _userService.GetOneUserDetails(UserID)));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{Login}")]
        [QueryStringConstraint("UserID", false)]
        [QueryStringConstraint("lob", true)]
        public ActionResult LoginUser(string lob)
        {
            AuthenticationHelper helper = new AuthenticationHelper();
            var token = _helper.GenerateToken(_jwtAuthentication.Value,"Srijit", "srijit.das@gmail.com","Admin");
            return Ok(APIResponse.CreateResponse(token,_userService.GetUserDetails()));
        }
        [HttpPost]
        [Authorize]
        [Route("register/login")]
        public ActionResult LoginUser(UserShortRegister userShortRegister)
        {
            var response = _userService.LoginUser(userShortRegister);
            var userResposne = response as UserModelResponse;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            if (userResposne != null)
            {
                if (token != null)
                {
                    token = _helper.GenerateToken(_jwtAuthentication.Value, userResposne.Data.ID.ToString(), userResposne.Data.Email, "Admin");
                }
                return Ok(APIResponse.CreateResponse(token, userResposne));
            }
            else
            {
                return Ok(APIResponse.CreateResponse(token, response));
            }
        }
        [HttpPost]
        [Authorize]
        [Route("register/socialLogin")]
        public ActionResult LoginSocialUser(UserModel userModel)
        {
            var response = _userService.LoginSocialUser(userModel);
            var userResposne = response as UserModelResponse;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            if (userResposne != null)
            {
                if (token != null)
                {
                    token = _helper.GenerateToken(_jwtAuthentication.Value, userResposne.Data.ID.ToString(), userResposne.Data.Email, "Admin");
                }
                return Ok(APIResponse.CreateResponse(token, userResposne));
            }
            else
            {
                return Ok(APIResponse.CreateResponse(token, response));
            }
        }
        [HttpPost]
        [Authorize]
        [Route("register/short-register")]
        public ActionResult CreateNewUser(UserShortRegister userShortRegister)
            {
            var response = _userService.CreateNewUser(userShortRegister);
            var userResposne = response as UserModelResponse;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            if (userResposne != null)
            {
                if (token != null)
                {
                    token = _helper.GenerateToken(_jwtAuthentication.Value, userResposne.Data.ID.ToString(), userResposne.Data.Email, "Admin");
                }
                return Ok(APIResponse.CreateResponse(token, userResposne));
            }
            else
            {
                return Ok(APIResponse.CreateResponse(token, response));
            }
        }
        [HttpPost]
        [Authorize]
        [Route("register/social-login")]
        public ActionResult CreateNewUserSocialLogin(UserRegister userRegister)
        {
            var response = _userService.CreateSocialUser(userRegister);
            var userResposne = response as UserModelResponse;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            if (userResposne != null)
            {
                if (token != null)
                {
                    token = _helper.GenerateToken(_jwtAuthentication.Value, userResposne.Data.ID.ToString(), userResposne.Data.Email, "Admin");
                }
                return Ok(APIResponse.CreateResponse(token, userResposne));
            }
            else
            {
                return Ok(APIResponse.CreateResponse(token, response));
            }
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [Authorize]
        [Route("register/user-update")]
        public ActionResult UpdateUser(UserRegister user)
        {
            var response = _userService.Register(user, user.GetType().Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [Authorize]
        [Route("register/user-basic-info")]
        public ActionResult UpdateUserBasicInfo(UserBasicInformation userBasic)
        {
            var response = _userService.Register(userBasic, userBasic.GetType().Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpPost]
        [Authorize]
        [Route("register/user-lifestyle")]
        public ActionResult UpdateUserLifeStyle(UserLifeStyleModel userlifeStyle)
        {
            var response = _userService.Register(userlifeStyle, userlifeStyle.GetType().Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        [Authorize]
        [Route("register/user-education-carrer")]
        public ActionResult UpdateUserEducation(UserEducationCareerModel userEducationCareer)
        {
            var response = _userService.Register(userEducationCareer, typeof(UserEducationCareerModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        [Authorize]
        [Route("register/user-religion")]
        public ActionResult UpdateUserReligion(UserReligionCasteModel userReligion)
        {
            var response = _userService.Register(userReligion, typeof(UserReligionCasteModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        [Authorize]
        [Route("register/user-preference")]
        public ActionResult UpdateUserPreference(UserPreferenceModel userPreference)
        {
            var response = _userService.Register(userPreference, typeof(UserPreferenceModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [Authorize]
        [Route("register/user-about")]
        public ActionResult UpdateUserAbout(UserAboutModel userAbout)
        {
            var response = _userService.Register(userAbout, typeof(UserAboutModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [Authorize]
        [Route("register/family-info")]
        public ActionResult UpdateUserFamilyInfo(UserFamilyInformationModel userFamily)
        {
            var response = _userService.Register(userFamily, typeof(UserFamilyInformationModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpPost]
        [Authorize]//Needs to be changed to Authorize
        [Route("register/images/{userId}")]
        public async Task<IActionResult> UploadImage(UserImagesUploadModel images, int userId)
        {
            //List<UserImage> userImages = new List<UserImage>();
            //images.ForEach(async img => {
            //    UserImage userImg = (UserImage)await _imageHandler.UploadUserImage(img.FormData);
            //    userImg.UserId = img.UserId;
            //    userImages.Add(img);
            //});
            //UserImage userImg = (UserImage)await _imageHandler.UploadUserImage(file);
            //userImg.UserId = userId;
            var response = await _userService.SaveImage(images, userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));

        }
        [HttpGet]
        [Authorize]
        [Route("interest-shortlisted/{userId}/{interestUserId}/{mode}/{isRemoved:int?}/{isRejected:int?}")]
        public async Task<IActionResult> InterestOrShortListed(int userId, int interestUserId, string mode, int isRemoved = 0, int isRejected = 0)
        {           
            var response = await _userService.InterestOrShortListed(userId, interestUserId, mode, isRemoved, isRejected);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("interest-shortlisted/{userId}/{interestUserId}")]
        public async Task<IActionResult> GetInterestShortListed(int userId, int interestUserId)
        {
            var response = await _userService.GetInterestShortListed(userId, interestUserId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("notification/{userId}")]
        public async Task<IActionResult> GetNotificationData(int userId)
        {
            var response = await _userService.GetNotificationData(userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("notification/Id/{id}")]
        public ActionResult UpdateNotificationData(int id)
        {
            var response = _userService.UpdateNotification(id);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        [Authorize]//Needs to be changed to Authorize
        [Route("user-list/{mode}")]
        public ActionResult GestUserList(SearchCritriaModel searchCritria, string mode)
        {
            var response = _userService.GestUserList(searchCritria, mode);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpGet]
        [Authorize]//Needs to be changed to Authorize
        [Route("user-details/{id}")]
        public ActionResult GestUser(int id)
        {
            var response = _userService.GetUserDetails(id);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("user-details/{userId}/{viewedId}")]
        public ActionResult GestUser(int userId, int viewedId)
        {
            var response = _userService.GetUserDetails(userId, viewedId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpPost]
        [Authorize]//Needs to be changed to Authorize
        [Route("chat/send-chat-invite")]
        public ActionResult SaveInvite(SendChatModel model)
        {
            var response = _userService.SaveChatInvite(model.SenderId, model.RevceiverId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpPost]
        [Authorize]//Needs to be changed to Authorize
        [Route("profile/quotient")]
        public ActionResult CheckProfileQuotient(SendChatModel model)
        {
            var response = _userService.GetProfileQuotient(model.SenderId, model.RevceiverId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("register/images/userID/{userId}/width/{width}/height/{height}/mode/{mode}")]
        public ActionResult GetImages(int userId, int width, int height, string mode)
        {
            var response = _userService.GetImages(userId, width, height, mode) as UserImageListResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        //private string BuildToken(string user, string email, string role)
        //{
        //    var claims = new[] {
        //        new Claim(JwtRegisteredClaimNames.Sub, user),
        //        new Claim(JwtRegisteredClaimNames.Email, email),
        //        new Claim(JwtRegisteredClaimNames.Birthdate, DateTime.Now.ToString("yyyy-MM-dd")),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //       };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(30),
        //      signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        [HttpGet]
        [Authorize]
        [Route("user-preference/{userId}")]
        public ActionResult GetPreference(int userId)
        {
            var response = _userService.GetUserPreferences(userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("user-profile-data/{userId}")]
        public ActionResult GetProfileDisplayData(int userId)
        {
            var response = _userService.GetProfileDisplayData(userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpGet]
        [Authorize]
        [Route("get-email-code/{userId}")]
        public ActionResult GetEmailCode(int userId)
        {
            var response = _userService.GenerateEmailCode(userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("verify-email/{userId}/{emailCode}")]
        public ActionResult VerifyEmail(int userId,string emailCode)
        {
            var response = _userService.VerfiyEmailCode(userId,emailCode);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]//Needs to be changed to Authorize
        [Route("send-otp-sms/{userId}")]
        public ActionResult SendOTPSMS(int userId)
        {
            var response = _userService.SendOTPSMS(userId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("verify-mobile/{userId}/{mobileCode}")]
        public ActionResult VerifyMobile(int userId, string mobileCode)
        {
            var response = _userService.VerfiyOTPSMS(userId, mobileCode);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
    }
}