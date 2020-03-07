using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class PreferenceMaster
    {
        public PreferenceMaster()
        {
            UserPreferenceSetting = new HashSet<UserPreferenceSetting>();
        }

        public int Id { get; set; }
        public string FieldName { get; set; }
        public bool Constrained { get; set; }
        public string DataType { get; set; }
        public int? ReferenceTableId { get; set; }

        public virtual MasterTableMetadata ReferenceTable { get; set; }
        public virtual ICollection<UserPreferenceSetting> UserPreferenceSetting { get; set; }
    }
}
