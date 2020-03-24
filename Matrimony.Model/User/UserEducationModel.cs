using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserEducationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? EducationLevelId { get; set; }
        public int? EducationFieldId { get; set; }
        public string Institution { get; set; }
        public string University { get; set; }
    }
}
