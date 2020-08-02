using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class InterestShortListedModel
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int InterestedUserId { get; set; }
        public bool IsShortListed { get; set; }
        public DateTime InterestDateTime { get; set; }
        public DateTime? ShortListedDateTime { get; set; }
        public bool IsInterestAccepted { get; set; }
        public bool IsInterestRejected { get; set; }
        public int? ShortListedBy { get; set; }
    }
}
