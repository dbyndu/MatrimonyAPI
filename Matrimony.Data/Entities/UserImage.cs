using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class UserImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsProfilePicture { get; set; }
        public int? ImageOrder { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
