using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MasterFieldValue
    {
        public MasterFieldValue()
        {
            UserInfoAnualIncome = new HashSet<UserInfo>();
            UserInfoBloodGroup = new HashSet<UserInfo>();
            UserInfoBodyType = new HashSet<UserInfo>();
            UserInfoCitizenship = new HashSet<UserInfo>();
            UserInfoCity = new HashSet<UserInfo>();
            UserInfoComplexion = new HashSet<UserInfo>();
            UserInfoComunity = new HashSet<UserInfo>();
            UserInfoCountry = new HashSet<UserInfo>();
            UserInfoEmployer = new HashSet<UserInfo>();
            UserInfoFamilyIncome = new HashSet<UserInfo>();
            UserInfoFamilyType = new HashSet<UserInfo>();
            UserInfoFamilyValues = new HashSet<UserInfo>();
            UserInfoFatherStatus = new HashSet<UserInfo>();
            UserInfoGender = new HashSet<UserInfo>();
            UserInfoHighestQualification = new HashSet<UserInfo>();
            UserInfoHighestSpecialization = new HashSet<UserInfo>();
            UserInfoMaritalStatus = new HashSet<UserInfo>();
            UserInfoMotherStatus = new HashSet<UserInfo>();
            UserInfoMotherTongue = new HashSet<UserInfo>();
            UserInfoReligion = new HashSet<UserInfo>();
            UserInfoSecondaryQualification = new HashSet<UserInfo>();
            UserInfoSecondarySpecialization = new HashSet<UserInfo>();
            UserInfoState = new HashSet<UserInfo>();
            UserInfoWorkDesignation = new HashSet<UserInfo>();
            UserInfoWorkingSector = new HashSet<UserInfo>();
            UserLifeStyleChildrenChoice = new HashSet<UserLifeStyle>();
            UserLifeStyleDiet = new HashSet<UserLifeStyle>();
            UserLifeStyleDrinking = new HashSet<UserLifeStyle>();
            UserLifeStyleHouseLivingIn = new HashSet<UserLifeStyle>();
            UserLifeStyleSmoking = new HashSet<UserLifeStyle>();
            UserLifeStyleWeadingStyle = new HashSet<UserLifeStyle>();
            UserPreferenceSetting = new HashSet<UserPreferenceSetting>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public int MasterTableId { get; set; }

        public virtual MasterTableMetadata MasterTable { get; set; }
        public virtual ICollection<UserInfo> UserInfoAnualIncome { get; set; }
        public virtual ICollection<UserInfo> UserInfoBloodGroup { get; set; }
        public virtual ICollection<UserInfo> UserInfoBodyType { get; set; }
        public virtual ICollection<UserInfo> UserInfoCitizenship { get; set; }
        public virtual ICollection<UserInfo> UserInfoCity { get; set; }
        public virtual ICollection<UserInfo> UserInfoComplexion { get; set; }
        public virtual ICollection<UserInfo> UserInfoComunity { get; set; }
        public virtual ICollection<UserInfo> UserInfoCountry { get; set; }
        public virtual ICollection<UserInfo> UserInfoEmployer { get; set; }
        public virtual ICollection<UserInfo> UserInfoFamilyIncome { get; set; }
        public virtual ICollection<UserInfo> UserInfoFamilyType { get; set; }
        public virtual ICollection<UserInfo> UserInfoFamilyValues { get; set; }
        public virtual ICollection<UserInfo> UserInfoFatherStatus { get; set; }
        public virtual ICollection<UserInfo> UserInfoGender { get; set; }
        public virtual ICollection<UserInfo> UserInfoHighestQualification { get; set; }
        public virtual ICollection<UserInfo> UserInfoHighestSpecialization { get; set; }
        public virtual ICollection<UserInfo> UserInfoMaritalStatus { get; set; }
        public virtual ICollection<UserInfo> UserInfoMotherStatus { get; set; }
        public virtual ICollection<UserInfo> UserInfoMotherTongue { get; set; }
        public virtual ICollection<UserInfo> UserInfoReligion { get; set; }
        public virtual ICollection<UserInfo> UserInfoSecondaryQualification { get; set; }
        public virtual ICollection<UserInfo> UserInfoSecondarySpecialization { get; set; }
        public virtual ICollection<UserInfo> UserInfoState { get; set; }
        public virtual ICollection<UserInfo> UserInfoWorkDesignation { get; set; }
        public virtual ICollection<UserInfo> UserInfoWorkingSector { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleChildrenChoice { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleDiet { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleDrinking { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleHouseLivingIn { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleSmoking { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleWeadingStyle { get; set; }
        public virtual ICollection<UserPreferenceSetting> UserPreferenceSetting { get; set; }
    }
}
