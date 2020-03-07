using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserCareer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? WorkingSectorId { get; set; }
        public int? WorkDesignationId { get; set; }
        public int? EmployerId { get; set; }
        public int? AnualIncomeId { get; set; }
        public bool? IsDisplayIncome { get; set; }

        public virtual MasterFieldValue AnualIncome { get; set; }
        public virtual MasterFieldValue Employer { get; set; }
        public virtual User User { get; set; }
        public virtual MasterFieldValue WorkDesignation { get; set; }
        public virtual MasterFieldValue WorkingSector { get; set; }
    }
}
