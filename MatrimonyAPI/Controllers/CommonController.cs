﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrimony.Service.Contracts;
using MatrimonyAPI.Authentication;
using MatrimonyAPI.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MatrimonyAPI.Controllers
{
    [EnableCors("AllowAll"), Route("api/[controller]")]
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
        public ActionResult MasterData()//coma separeted table names
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string token = string.Empty;

            if (string.IsNullOrEmpty(accessToken))
            {
                token = _helper.GenerateToken(_jwtAuthentication.Value, "default", "default@default.com", "User");
            }
            else
            {
                var tokeValue = accessToken.ToString().Split(new[] { ' ' }, 2);
                if(tokeValue!=null && tokeValue.Length > 1 && tokeValue[1].ToString()!="null")
                {
                    token = _helper.ValidateToken(_jwtAuthentication.Value, accessToken);
                }
                else
                {
                    token = _helper.GenerateToken(_jwtAuthentication.Value, "default", "default@default.com", "User");
                }
                
            }
            //List<string> lstTable = tableNames.Split(',').ToList();
            return Ok(APIResponse.CreateResponse(token, _masterDataService.GetMasterDate()));
        }
        [HttpGet]
        [Authorize]
        [Route("country")]
        public ActionResult GetCountry()
        {
            var response = _masterDataService.GetCountry();
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("state/{countryId}")]
        public ActionResult GetState(int countryId)
        {
            var response = _masterDataService.GetState(countryId);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
        [HttpGet]
        [Authorize]
        [Route("city/{stateIds}")]
        public ActionResult GetCity(string stateIds)
        {
            var response = _masterDataService.GetCity(stateIds);
            return Ok(APIResponse.CreateResponse(_jwtAuthentication.Value, _httpContextAccessor.HttpContext.Request, response));
        }
    }
}