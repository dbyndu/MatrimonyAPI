using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserModel : UserRegister
    {        
        //public UserBasicInformation UserBasicInfo { get; set; }
        //public ICollection<UserCareerModel> UserCareer { get; set; }
        //public ICollection<UserEducationModel> UserEducation { get; set; }
        //public UserFamilyInformationModel UserFamilyInfo { get; set; }
        //public UserLifeStyleModel UserLifeStyle { get; set; }
        //public UserLocationModel UserLocation { get; set; }
        public virtual ICollection<UserBasicInformation> UserBasicInfo { get; set; }
        public virtual ICollection<UserCareerModel> UserCareer { get; set; }
        public virtual ICollection<UserEducationModel> UserEducation { get; set; }
        public virtual ICollection<UserFamilyInformationModel> UserFamilyInfo { get; set; }
        public virtual ICollection<UserLifeStyleModel> UserLifeStyle { get; set; }
        public virtual ICollection<UserLocationModel> UserLocation { get; set; }
    }
}
