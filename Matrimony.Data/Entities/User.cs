using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserImage = new HashSet<UserImage>();
            UserInfo = new HashSet<UserInfo>();
            UserLifeStyle = new HashSet<UserLifeStyle>();
            UserPreferenceSetting = new HashSet<UserPreferenceSetting>();
        }

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

        public virtual ICollection<UserImage> UserImage { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyle { get; set; }
        public virtual ICollection<UserPreferenceSetting> UserPreferenceSetting { get; set; }
    }
}
