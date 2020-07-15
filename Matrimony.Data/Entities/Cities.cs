using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
