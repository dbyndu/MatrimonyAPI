using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserModel : UserBasicModel
    {
        public string FirstName { get; set; }
        public string MiddleNmae { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int ProfileCreatedForId { get; set; }
        public string ContactName { get; set; }
        public UserBasicInformation BasicInfo { get; set; }
        public ICollection<UserCareerModel> UserCareer { get; set; }
        public ICollection<UserEducationModel> UserEducation { get; set; }
        public UserFamilyInformationModel UserFamilyInfo { get; set; }
        public UserLifeStyleModel UserLifeStyle { get; set; }
        public UserLocationModel UserLocation { get; set; }
    }
}
