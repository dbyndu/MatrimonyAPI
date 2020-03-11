using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.Base;

namespace Matrimony.Service.Contracts
{
    public interface IMasterDataService
    {
        Response GetMasterDate(List<string> tableNames);
    }
}
