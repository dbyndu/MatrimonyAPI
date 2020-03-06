using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Base
{
    public class Error
    {
        private Error() { }


        //public constractor for Error object
        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
