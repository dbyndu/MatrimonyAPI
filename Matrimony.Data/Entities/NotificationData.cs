using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Data.Entities
{
    public class NotificationData
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsSeen { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? SeenDateTime { get; set; }
        public int SenderId { get; set; }
    }
}
