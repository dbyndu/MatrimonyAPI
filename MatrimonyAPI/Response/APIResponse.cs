using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Matrimony.Model.Base;

namespace MatrimonyAPI.Response
{
    public sealed class APIResponse
    {
        string _token;
        ResponseType _response;

        public static ResponseType CreateResponse(string token, ResponseType response)
        {
            if (response != null)
            {
                response.Metadata.Token = token;
                return response;
            }
            return null;
        }
    }
}
