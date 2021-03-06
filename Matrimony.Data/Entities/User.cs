﻿using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleNmae { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int ProfileCreatedForId { get; set; }
        public string ContactName { get; set; }
        public int? PercentageComplete { get; set; }
        public bool? IsSocialLogin { get; set; }
        public string SocialId { get; set; }
        public int? ProviderId { get; set; }
        public bool? IsMobileVerified { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string PhoneCountryCode { get; set; }
        public bool? IsActive { get; set; }
        public string CompletePhoneNumber { get; set; }
    }
}
