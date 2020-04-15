using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.Common
{
    public class SearchCritriaModel
    {
        public int UserId { get; set; }
        public int Gender { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int Religion { get; set; }
        public int MotherTongue { get; set; }
    }
}
