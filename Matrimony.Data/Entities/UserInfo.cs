using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserInfo
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
        public int? BloodGroupId { get; set; }
        public int? ReligionId { get; set; }
        public int? CasteId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? ComunityId { get; set; }
        public string Gothra { get; set; }
        public bool? IsIgnorCast { get; set; }
        public int? CountryId { get; set; }
        public int? CitizenshipId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string GrewUpIn { get; set; }
        public string Origin { get; set; }
        public long? Pin { get; set; }
        public int? HighestQualificationId { get; set; }
        public int? HighestSpecializationId { get; set; }
        public int? SecondaryQualificationId { get; set; }
        public int? SecondarySpecializationId { get; set; }
        public string Institution { get; set; }
        public string University { get; set; }
        public int? WorkingSectorId { get; set; }
        public int? WorkDesignationId { get; set; }
        public int? EmployerId { get; set; }
        public int? AnualIncomeId { get; set; }
        public bool? IsDisplayIncome { get; set; }
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
        public string About { get; set; }
        public bool? Dosh { get; set; }
        public bool? Manglik { get; set; }
        public bool? Horoscope { get; set; }

        public virtual MasterFieldValue AnualIncome { get; set; }
        public virtual MasterFieldValue BloodGroup { get; set; }
        public virtual MasterFieldValue BodyType { get; set; }
        public virtual MasterFieldValue Citizenship { get; set; }
        public virtual Cities City { get; set; }
        public virtual MasterFieldValue Complexion { get; set; }
        public virtual MasterFieldValue Comunity { get; set; }
        public virtual Countries Country { get; set; }
        public virtual MasterFieldValue Employer { get; set; }
        public virtual MasterFieldValue FamilyIncome { get; set; }
        public virtual MasterFieldValue FamilyType { get; set; }
        public virtual MasterFieldValue FamilyValues { get; set; }
        public virtual MasterFieldValue FatherStatus { get; set; }
        public virtual MasterFieldValue Gender { get; set; }
        public virtual MasterFieldValue HighestQualification { get; set; }
        public virtual MasterFieldValue HighestSpecialization { get; set; }
        public virtual MasterFieldValue MaritalStatus { get; set; }
        public virtual MasterFieldValue MotherStatus { get; set; }
        public virtual MasterFieldValue MotherTongue { get; set; }
        public virtual MasterFieldValue Religion { get; set; }
        public virtual MasterFieldValue SecondaryQualification { get; set; }
        public virtual MasterFieldValue SecondarySpecialization { get; set; }
        public virtual States State { get; set; }
        public virtual User User { get; set; }
        public virtual MasterFieldValue WorkDesignation { get; set; }
        public virtual MasterFieldValue WorkingSector { get; set; }
    }
}
