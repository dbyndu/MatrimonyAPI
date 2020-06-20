using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class ProfileDisplayData
    {
        public string ProfileImageString { get; set; }
        public int NewMatchCount { get; set; }
        public int RecentlyViewed { get; set; }
        public int ShortListed { get; set; }
        public int Interest { get; set; }
    }
}
