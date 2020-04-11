using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class States
    {
        public States()
        {
            Cities = new HashSet<Cities>();
            UserInfo = new HashSet<UserInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
