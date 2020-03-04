using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatrimonyAPI.Response
{
    public class APIResponse : IActionResult
    {
        string _value;
        HttpRequestMessage _request;

        public APIResponse(string value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }
        public Task ExecuteResultAsync(ActionContext context)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}
