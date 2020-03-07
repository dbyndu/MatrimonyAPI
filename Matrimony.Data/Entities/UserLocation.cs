using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserLocation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string City { get; set; }
        public string GrewUpIn { get; set; }
        public string Origin { get; set; }
        public long? Pin { get; set; }

        public virtual MasterFieldValue Country { get; set; }
        public virtual MasterFieldValue State { get; set; }
        public virtual User User { get; set; }
    }
}
