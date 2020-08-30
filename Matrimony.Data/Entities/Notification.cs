using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public int NotificationSubTypeId { get; set; }
        public bool? IsSeen { get; set; }
        public bool? IsRead { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? SeenDateTime { get; set; }
    }
}
