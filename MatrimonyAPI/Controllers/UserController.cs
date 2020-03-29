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
        public UserController(IConfiguration config,ILogger<UserController> logger, IUserDetailsService userService, IOptions<JwtAuthentication> jwtAuthentication, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _logger = logger;
            _userService = userService;
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
            _httpContextAccessor = httpContextAccessor;
            _helper = new AuthenticationHelper();
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
        [Route("register/user-education")]
        public ActionResult UpdateUserEducation(List<UserEducationModel> userEducations)
        {
            var response = _userService.Register(userEducations, typeof(UserEducationModel).Name) as UserModelResponse;
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpPost]
        //Needs to be changed to Authorize
        [AllowAnonymous]
        [Route("register/user-career")]
        public ActionResult UpdateUserCareer(List<UserCareerModel> userCareer)
        {
            var response = _userService.Register(userCareer, typeof(UserCareerModel).Name) as UserModelResponse;
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
    }
}