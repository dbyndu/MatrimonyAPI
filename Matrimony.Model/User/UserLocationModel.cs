﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserLocationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string City { get; set; }
        public string GrewUpIn { get; set; }
        public string Origin { get; set; }
        public long? Pin { get; set; }
    }
}
