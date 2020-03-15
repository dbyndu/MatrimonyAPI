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
using Matrimony_Model.User;
using Microsoft.AspNetCore.Cors;

namespace MatrimonyAPI.Controllers
{
    //[Authorize]
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("[controller]")]
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
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
            return Ok(APIResponse.CreateResponse(token, _userService.GetOneUserDetails(UserID)));
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
        [AllowAnonymous]
        [Route("{register}")]
        public ActionResult RegisterUser(UserShortRegister userShortRegister)
        {
            var response = _userService.CreateNewUser(userShortRegister) as UserModelResponse;
            var token = _helper.GenerateToken(_jwtAuthentication.Value, response.Data.FirstName, response.Data.Email, "User");
            return Ok(APIResponse.CreateResponse(token, response));
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