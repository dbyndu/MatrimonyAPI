using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserVerification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? EmailVerificationCode { get; set; }
        public int? MobileVerificationCode { get; set; }
        public DateTime? EmailCodeGenDateTime { get; set; }
        public DateTime? MobileCodeGenDateTime { get; set; }
        public DateTime? ProfileLoginLogged { get; set; }
        public DateTime? ProfileLogoutLogged { get; set; }
    }
}
