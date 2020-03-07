using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserBasicInfo = new HashSet<UserBasicInfo>();
            UserCareer = new HashSet<UserCareer>();
            UserEducation = new HashSet<UserEducation>();
            UserFamilyInfo = new HashSet<UserFamilyInfo>();
            UserLifeStyle = new HashSet<UserLifeStyle>();
            UserLocation = new HashSet<UserLocation>();
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

        public virtual ICollection<UserBasicInfo> UserBasicInfo { get; set; }
        public virtual ICollection<UserCareer> UserCareer { get; set; }
        public virtual ICollection<UserEducation> UserEducation { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfo { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyle { get; set; }
        public virtual ICollection<UserLocation> UserLocation { get; set; }
        public virtual ICollection<UserPreferenceSetting> UserPreferenceSetting { get; set; }
    }
}
