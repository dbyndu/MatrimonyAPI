using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserEducationCareerModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
        public string OtherEmployer { get; set; }
    }
}
