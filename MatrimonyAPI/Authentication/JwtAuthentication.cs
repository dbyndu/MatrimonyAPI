using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrimonyAPI.Authentication
{
    public class JwtAuthentication
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string audience { get; set; }

        public string Expires { get; set; }
    }
}
