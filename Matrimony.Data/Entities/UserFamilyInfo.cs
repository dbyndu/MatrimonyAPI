using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserFamilyInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? FatherStatusId { get; set; }
        public int? MotherStatusId { get; set; }
        public string NativePlace { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string FamilyLocation { get; set; }
        public int? MarriedSiblingMale { get; set; }
        public int? NotMarriedSiblingMale { get; set; }
        public int? MarriedSiblingFemale { get; set; }
        public int? NotMarriedSiblingFemale { get; set; }
        public int? FamilyTypeId { get; set; }
        public int? FamilyValuesId { get; set; }
        public int? FamilyAffluenceId { get; set; }

        public virtual MasterFieldValue FamilyAffluence { get; set; }
        public virtual MasterFieldValue FamilyType { get; set; }
        public virtual MasterFieldValue FamilyValues { get; set; }
        public virtual MasterFieldValue FatherStatus { get; set; }
        public virtual MasterFieldValue MotherStatus { get; set; }
        public virtual User User { get; set; }
    }
}
