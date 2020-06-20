﻿using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class MessageRoom
    {
        public MessageRoom()
        {
            Message = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool? IsChatActive { get; set; }
        public DateTime? DateTimeLogged { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool? IsBlocked { get; set; }

        public virtual ICollection<Message> Message { get; set; }
    }
}
