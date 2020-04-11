using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony_Model.Common
{
    public class MasterDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MasterTableId { get; set; }
        public string TableName { get; set; }
    }
}
