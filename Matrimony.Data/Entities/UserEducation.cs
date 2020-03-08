using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserEducation
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? EducationLevelId { get; set; }
        public int? EducationFieldId { get; set; }
        public string Institution { get; set; }
        public string University { get; set; }

        public virtual MasterFieldValue EducationField { get; set; }
        public virtual MasterFieldValue EducationLevel { get; set; }
        public virtual User User { get; set; }
    }
}
