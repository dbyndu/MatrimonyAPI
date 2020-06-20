using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Data.Entities
{
    public class ProfileDisplayData
    {       
        public byte[] ProfileDisplayPicture { get; set; }
        public string ContentType { get; set; }
        public int NewMatchCount { get; set; }
        public int RecentlyViewed { get; set; }
        public int ShortListed { get; set; }
        public int Interest { get; set; }
    }
}
