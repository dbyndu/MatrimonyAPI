using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserLifeStyleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? DietId { get; set; }
        public string Hobies { get; set; }
        public int? SmokingId { get; set; }
        public int? ChildrenChoiceId { get; set; }
        public int? WeadingStyleId { get; set; }
    }
}
