using System;
using System.Collections.Generic;

namespace Matrimony.Data.Entities
{
    public partial class Countries
    {
        public int Id { get; set; }
        public string Sortname { get; set; }
        public string Name { get; set; }
        public string Phonecode { get; set; }
    }
}
