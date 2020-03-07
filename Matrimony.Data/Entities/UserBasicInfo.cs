using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserBasicInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GenderId { get; set; }
        public DateTime Dob { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? HealthInfoId { get; set; }
        public bool? IsDisability { get; set; }
        public int? BloodGroupId { get; set; }
        public int? ReligionId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? ComunityId { get; set; }
        public string Gothra { get; set; }
        public bool? IsIgnorCast { get; set; }
        public string About { get; set; }

        public virtual MasterFieldValue BloodGroup { get; set; }
        public virtual MasterFieldValue Comunity { get; set; }
        public virtual MasterFieldValue Gender { get; set; }
        public virtual MasterFieldValue HealthInfo { get; set; }
        public virtual MasterFieldValue MaritalStatus { get; set; }
        public virtual MasterFieldValue MotherTongue { get; set; }
        public virtual MasterFieldValue Religion { get; set; }
        public virtual User User { get; set; }
    }
}
