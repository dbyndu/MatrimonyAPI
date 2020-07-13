using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserProfileCompletion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool? ShortRegisterMandatory { get; set; }
        public bool? RegisterMandatory { get; set; }
        public bool? PhotoUpload { get; set; }
        public bool? BasicDetailsMandatory { get; set; }
        public bool? BasicDetailsOptional { get; set; }
        public bool? ReligionMandatory { get; set; }
        public bool? ReligionOptional { get; set; }
        public bool? CareerMandatory { get; set; }
        public bool? CareerOptional { get; set; }
        public bool? FamilyMandatory { get; set; }
        public bool? FamilyOptional { get; set; }
        public bool? About { get; set; }
        public bool? LifeStyleMandatory { get; set; }
        public bool? LifeStyleOptional { get; set; }
        public bool? PreferenceMandatory { get; set; }
        public bool? PreferenceOptional { get; set; }
    }
}
