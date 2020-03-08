using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MasterFieldValue
    {
        public MasterFieldValue()
        {
            UserBasicInfoBloodGroup = new HashSet<UserBasicInfo>();
            UserBasicInfoComunity = new HashSet<UserBasicInfo>();
            UserBasicInfoGender = new HashSet<UserBasicInfo>();
            UserBasicInfoHealthInfo = new HashSet<UserBasicInfo>();
            UserBasicInfoMaritalStatus = new HashSet<UserBasicInfo>();
            UserBasicInfoMotherTongue = new HashSet<UserBasicInfo>();
            UserBasicInfoReligion = new HashSet<UserBasicInfo>();
            UserCareerAnualIncome = new HashSet<UserCareer>();
            UserCareerEmployer = new HashSet<UserCareer>();
            UserCareerWorkDesignation = new HashSet<UserCareer>();
            UserCareerWorkingSector = new HashSet<UserCareer>();
            UserEducationEducationField = new HashSet<UserEducation>();
            UserEducationEducationLevel = new HashSet<UserEducation>();
            UserFamilyInfoFamilyAffluence = new HashSet<UserFamilyInfo>();
            UserFamilyInfoFamilyType = new HashSet<UserFamilyInfo>();
            UserFamilyInfoFamilyValues = new HashSet<UserFamilyInfo>();
            UserFamilyInfoFatherStatus = new HashSet<UserFamilyInfo>();
            UserFamilyInfoMotherStatus = new HashSet<UserFamilyInfo>();
            UserLifeStyleChildrenChoice = new HashSet<UserLifeStyle>();
            UserLifeStyleDiet = new HashSet<UserLifeStyle>();
            UserLifeStyleSmoking = new HashSet<UserLifeStyle>();
            UserLifeStyleWeadingStyle = new HashSet<UserLifeStyle>();
            UserLocationCountry = new HashSet<UserLocation>();
            UserLocationState = new HashSet<UserLocation>();
            UserPreferenceSetting = new HashSet<UserPreferenceSetting>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public int MasterTableId { get; set; }

        public virtual MasterTableMetadata MasterTable { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoBloodGroup { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoComunity { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoGender { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoHealthInfo { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoMaritalStatus { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoMotherTongue { get; set; }
        public virtual ICollection<UserBasicInfo> UserBasicInfoReligion { get; set; }
        public virtual ICollection<UserCareer> UserCareerAnualIncome { get; set; }
        public virtual ICollection<UserCareer> UserCareerEmployer { get; set; }
        public virtual ICollection<UserCareer> UserCareerWorkDesignation { get; set; }
        public virtual ICollection<UserCareer> UserCareerWorkingSector { get; set; }
        public virtual ICollection<UserEducation> UserEducationEducationField { get; set; }
        public virtual ICollection<UserEducation> UserEducationEducationLevel { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfoFamilyAffluence { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfoFamilyType { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfoFamilyValues { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfoFatherStatus { get; set; }
        public virtual ICollection<UserFamilyInfo> UserFamilyInfoMotherStatus { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleChildrenChoice { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleDiet { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleSmoking { get; set; }
        public virtual ICollection<UserLifeStyle> UserLifeStyleWeadingStyle { get; set; }
        public virtual ICollection<UserLocation> UserLocationCountry { get; set; }
        public virtual ICollection<UserLocation> UserLocationState { get; set; }
        public virtual ICollection<UserPreferenceSetting> UserPreferenceSetting { get; set; }
    }
}
