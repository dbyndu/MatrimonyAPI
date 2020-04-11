using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Countries
    {
        public Countries()
        {
            States = new HashSet<States>();
            UserInfo = new HashSet<UserInfo>();
        }

        public int Id { get; set; }
        public string Sortname { get; set; }
        public string Name { get; set; }
        public string Phonecode { get; set; }

        public virtual ICollection<States> States { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
