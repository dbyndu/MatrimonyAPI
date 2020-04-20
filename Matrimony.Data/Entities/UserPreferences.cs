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
        public int? MaritialStatus { get; set; }
        public int? Country { get; set; }
        public int? Citizenship { get; set; }
        public int? State { get; set; }
        public string City { get; set; }
        public int? Religion { get; set; }
        public string MotherTongue { get; set; }
        public int? Caste { get; set; }
        public int? Subcaste { get; set; }
        public int? Gothram { get; set; }
        public bool? Dosh { get; set; }
        public bool? Manglik { get; set; }
        public bool? Horoscope { get; set; }
        public int? HighestQualification { get; set; }
        public bool? Working { get; set; }
        public string Occupation { get; set; }
        public string Specialization { get; set; }
        public int? AnnualIncome { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
