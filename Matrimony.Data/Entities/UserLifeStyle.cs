using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserLifeStyle
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? DietId { get; set; }
        public string Hobies { get; set; }
        public int? SmokingId { get; set; }
        public int? ChildrenChoiceId { get; set; }
        public int? WeadingStyleId { get; set; }

        public virtual MasterFieldValue ChildrenChoice { get; set; }
        public virtual MasterFieldValue Diet { get; set; }
        public virtual MasterFieldValue Smoking { get; set; }
        public virtual User User { get; set; }
        public virtual MasterFieldValue WeadingStyle { get; set; }
    }
}
