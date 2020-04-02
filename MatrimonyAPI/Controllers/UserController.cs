﻿using System;
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
        [Route("register/short-register")]
        public ActionResult CreateNewUser(UserShortRegister userShortRegister)
        {
            var response = _userService.CreateNewUser(userShortRegister) as UserModelResponse;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            if(token != null)
            {
                token = _helper.GenerateToken(_jwtAuthentication.Value, response.Data.ContactName, response.Data.Email, "Admin");
            }
            return Ok(APIResponse.CreateResponse(token, response));
        }

        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-update")]
        public ActionResult UpdateUser(UserRegister user)
        {
            var response = _userService.Register(user, user.GetType().Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-basic-info")]
        public ActionResult UpdateUserBasicInfo(UserBasicInformation userBasic)
        {
            var response = _userService.Register(userBasic, userBasic.GetType().Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-education-carrer")]
        public ActionResult UpdateUserEducation(UserEducationCareerModel userEducationCareer)
        {
            var response = _userService.Register(userEducationCareer, typeof(UserEducationCareerModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-religion")]
        public ActionResult UpdateUserCareer(UserReligionCasteModel userReligion)
        {
            var response = _userService.Register(userReligion, typeof(UserReligionCasteModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-about")]
        public ActionResult UpdateUserCareer(UserAboutModel userAbout)
        {
            var response = _userService.Register(userAbout, typeof(UserAboutModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/family-info")]
        public ActionResult UpdateUserFamilyInfo(UserFamilyInformationModel userFamily)
        {
            var response = _userService.Register(userFamily, typeof(UserFamilyInformationModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }

        [HttpPost]
        [AllowAnonymous]//Needs to be changed to Authorize
        [Route("register/images/{userId}")]
        public async Task<IActionResult> UploadImage(IFormFile file, int userId)
        {
            //userId = 9;
            UserImage userImg = (UserImage)await _imageHandler.UploadUserImage(file);
            userImg.UserId = userId;
            var response = _userService.Register(userImg, typeof(UserImage).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));

        }
        [HttpGet]
        [AllowAnonymous]//Needs to be changed to Authorize
        [Route("user-list")]
        public ActionResult GestUserList()
        {
            var response = _userService.GestUserList();
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
    }
}