using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserForgetPassword
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }
        public string VerficationCode { get; set; }
        public string ModelStatus { get; set; }
    }

    public class UserChangePassword
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ModelStatus { get; set; }
    }
}
