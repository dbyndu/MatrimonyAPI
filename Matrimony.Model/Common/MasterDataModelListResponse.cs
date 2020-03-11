using Matrimony.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony_Model.Common
{
    public class MasterDataModelListResponse : SuccessResponse<List<MasterDataModel>>
    {
        public MasterDataModelListResponse(Metadata metadata, List<MasterDataModel> masterDataModel)
            : base(metadata, masterDataModel)
        { }

    }
}
