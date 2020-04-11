using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Cities
    {
        public Cities()
        {
            UserInfo = new HashSet<UserInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public virtual States State { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
