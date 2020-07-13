using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class States
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
