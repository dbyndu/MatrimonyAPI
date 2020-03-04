using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrimonyAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Matrimony.Service.Contracts;
using Models.Base;
using Models.UserModels;

namespace MatrimonyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDetailsService _userService;
        public UserController(ILogger<UserController> logger, IUserDetailsService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            //APIResponse aPIResponse = new APIResponse("Created", new HttpRequest().HttpContext);
            return Ok("Created");
        }
        [HttpGet]
        public ActionResult GetUser(int blabla)
        {

            return Ok(_userService.GetUserDetails());
        }
    }
}