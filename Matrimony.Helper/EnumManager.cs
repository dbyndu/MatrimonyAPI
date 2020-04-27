using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Helper
{
    public class EnumManager
    {
        public enum ProfileProgress
        {
            Increase,
            Decrease,
            NoChange
        }
        public enum AvailableProfiles
        {
            ShortRegistration,
            Registration,
            Image,
            BasicDetails,
            ReligionCaste,
            CareerEducation,
            FamilyDetails,
            About,
            LifeStyle,
            Preference
        }
        public enum ProfileCriteria
        {
            Mandatory,
            Optional,
            All
        }
    }
}
