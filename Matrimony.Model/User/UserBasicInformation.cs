﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserBasicInformation
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
    }
}
