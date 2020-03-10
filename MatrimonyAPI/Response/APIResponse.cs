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
        Matrimony.Model.Base.Response _response;

        public static Matrimony.Model.Base.Response CreateResponse(string token, Matrimony.Model.Base.Response response)
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
