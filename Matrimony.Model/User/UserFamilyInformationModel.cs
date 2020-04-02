using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserFamilyInformationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? FatherStatusId { get; set; }
        public int? MotherStatusId { get; set; }
        public string NativePlace { get; set; }
        public string FamilyLocation { get; set; }
        public int? MarriedSiblingMale { get; set; }
        public int? NotMarriedSiblingMale { get; set; }
        public int? MarriedSiblingFemale { get; set; }
        public int? NotMarriedSiblingFemale { get; set; }
        public int? FamilyTypeId { get; set; }
        public int? FamilyValuesId { get; set; }
        public int? FamilyIncomeId { get; set; }
    }
}
