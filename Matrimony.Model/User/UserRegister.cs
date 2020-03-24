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
    }
}
