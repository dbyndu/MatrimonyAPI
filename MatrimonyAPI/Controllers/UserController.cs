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

namespace MatrimonyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDetailsService _userService;
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;
        public UserController(ILogger<UserController> logger, IUserDetailsService userService, IOptions<JwtAuthentication> jwtAuthentication)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
        }

        [HttpGet]
        //public ActionResult Register()
        //{
        //    //APIResponse aPIResponse = new APIResponse("Created", new HttpRequest().HttpContext);
        //    return Ok("Created");
        //}
        [HttpGet]
        public ActionResult GetUser(int blabla)
        {

            return Ok(_userService.GetUserDetails());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{Login}")]
        public ActionResult LoginUser()
        {
            AuthenticationHelper helper = new AuthenticationHelper();
            var token = helper.GenerateToken(_jwtAuthentication.Value, "Srijit", "srijit.das@gmail.com");
            return Ok(_userService.GetUserDetails());
        }
    }
}