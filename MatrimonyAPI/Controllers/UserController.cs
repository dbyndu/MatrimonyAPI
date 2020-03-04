using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrimonyAPI.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MatrimonyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Register()
        {
            //APIResponse aPIResponse = new APIResponse("Created", new HttpRequest().HttpContext);
            return Ok("Created");
        }
    }
}