using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserBasicInformation : UserBasicModel
    {
        public string GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MaritalStatusId { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public int HealthInfoId { get; set; }
        public bool IsDisability { get; set; }
        public string BloodGroup { get; set; }
        public int ReligionId { get; set; }
        public int MotherTongueId { get; set; }
        public int Community { get; set; }
        public string Gothra { get; set; }
        public int IsIgnoreCast { get; set; }
        public string About { get; set; }
    }
}
