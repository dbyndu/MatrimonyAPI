using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Data.Entities
{
    public partial class UserImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        public virtual User User { get; set; }
    }
}
