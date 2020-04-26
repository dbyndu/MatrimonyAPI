using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserReligionCasteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ReligionId { get; set; }
        public int? CasteId { get; set; }
        public string Gothra { get; set; }
        public bool? IsIgnorCast { get; set; }
        public bool? Dosh { get; set; }
        public bool? Manglik { get; set; }
        public bool? Horoscope { get; set; }
    }
}
