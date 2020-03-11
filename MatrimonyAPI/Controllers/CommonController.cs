using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrimony.Service.Contracts;
using MatrimonyAPI.Authentication;
using MatrimonyAPI.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MatrimonyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;
        private readonly IMasterDataService _masterDataService;
        private readonly IOptions<JwtAuthentication> _jwtAuthentication;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthenticationHelper _helper;
        private IConfiguration _config;
        public CommonController(IConfiguration config, ILogger<CommonController> logger, IMasterDataService masterDataService, IOptions<JwtAuthentication> jwtAuthentication, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _logger = logger;
            _masterDataService = masterDataService;
            _jwtAuthentication = jwtAuthentication ?? throw new ArgumentNullException(nameof(jwtAuthentication));
            _httpContextAccessor = httpContextAccessor;
            _helper = new AuthenticationHelper();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("MasterData")]
        public ActionResult LoginUser(string tableNames)//coma separeted table names
        {
            AuthenticationHelper helper = new AuthenticationHelper();
            var token = _helper.GenerateToken(_jwtAuthentication.Value, "Srijit", "srijit.das@gmail.com", "Admin");
            List<string> lstTable = tableNames.Split(',').ToList();
            return Ok(APIResponse.CreateResponse(token, _masterDataService.GetMasterDate(lstTable)));
        }
    }
}