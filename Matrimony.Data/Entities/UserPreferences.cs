using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserPreferences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? HeightFrom { get; set; }
        public int? HeightTo { get; set; }
        public string MaritalStatus { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Religion { get; set; }
        public string MotherTongue { get; set; }
        public string Caste { get; set; }
        public bool? Dosh { get; set; }
        public bool? Manglik { get; set; }
        public string HighestQualification { get; set; }
        public string Occupation { get; set; }
        public string Specialization { get; set; }
        public int? AnnualIncome { get; set; }
        public bool? IsAccepted { get; set; }

        public virtual User User { get; set; }
    }
}
