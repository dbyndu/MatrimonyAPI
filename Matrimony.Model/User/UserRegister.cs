using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Matrimony.Model.User
{
    public class UserRegister: UserShortRegister
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleNmae { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }
        public string ContactName { get; set; }
        public int? PercentageComplete { get; set; }
        public bool? IsSocialLogin { get; set; }
        public string SocialId { get; set; }
        public int? ProviderId { get; set; }
        public int? genderId { get; set; }
        public bool? IsMobileVerified { get; set; }
        public bool? IsEmailVerified { get; set; }
    }
}
