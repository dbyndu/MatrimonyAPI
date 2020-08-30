using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserPercentageComplete
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool BasicRegister { get; set; }
        public bool Register { get; set; }
        public bool PhotoUpload { get; set; }
        public bool BasicDetails{ get; set; }
        public bool Religion{ get; set; }
        public bool Career{ get; set; }
        public bool Family { get; set; }
        public bool About { get; set; }
        public bool LifeStyle{ get; set; }
        public bool Preference{ get; set; }
    }
}
