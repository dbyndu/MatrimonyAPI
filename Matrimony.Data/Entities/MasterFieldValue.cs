using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MasterFieldValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int MasterTableId { get; set; }
        public int? DependentTableId { get; set; }
        public int? DependentFieldId { get; set; }
    }
}
