﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserReligionCasteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ReligionId { get; set; }
        public string Caste { get; set; }
        public string Gothra { get; set; }
        public bool? IsIgnorCast { get; set; }
    }
}
