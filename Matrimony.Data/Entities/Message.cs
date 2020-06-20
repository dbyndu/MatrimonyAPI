using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public Guid? RoomId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Message1 { get; set; }
        public bool? IsSeen { get; set; }
        public string OfflineUserId { get; set; }

        public virtual MessageRoom Room { get; set; }
    }
}
