using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MasterTableMetadata
    {
        public MasterTableMetadata()
        {
            MasterFieldValue = new HashSet<MasterFieldValue>();
            PreferenceMaster = new HashSet<PreferenceMaster>();
        }

        public int Id { get; set; }
        public string TableName { get; set; }

        public virtual ICollection<MasterFieldValue> MasterFieldValue { get; set; }
        public virtual ICollection<PreferenceMaster> PreferenceMaster { get; set; }
    }
}
