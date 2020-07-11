using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class RecentlyViewed
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ViewedId { get; set; }
        public DateTime ViewDateTime { get; set; }
    }
}
