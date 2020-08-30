using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MessageRoom
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool? IsChatActive { get; set; }
        public DateTime? DateTimeLogged { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
