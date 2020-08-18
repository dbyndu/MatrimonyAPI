using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserBasicInformation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? GenderId { get; set; }
        public DateTime? Dob { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? BodyTypeId { get; set; }
        public int? ComplexionId { get; set; }
        public bool? IsDisability { get; set; }
        public bool? Dosh { get; set; }
        public bool? Manglik { get; set; }
        public bool? Horoscope { get; set; }
        public int? BloodGroupId { get; set; }
        public int? ReligionId { get; set; }
        public int? CasteId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? ComunityId { get; set; }
        public string Gothra { get; set; }
        //public bool? IsIgnorCast { get; set; }
        public int? CountryId { get; set; }
        public int? CitizenshipId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string GrewUpIn { get; set; }
        public string Origin { get; set; }
        public long? Pin { get; set; }
        public string About { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

    }
}
