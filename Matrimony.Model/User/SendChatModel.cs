using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class SendChatModel
    {
        public int SenderId { get; set; }
        public int RevceiverId { get; set; }
        public int mode { get; set; }
    }
}
