using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserPreferenceSetting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PreferenceMasterId { get; set; }
        public int? FieldValueId { get; set; }
        public string UnconstraineValue { get; set; }

        public virtual MasterFieldValue FieldValue { get; set; }
        public virtual PreferenceMaster PreferenceMaster { get; set; }
        public virtual User User { get; set; }
    }
}
