using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Model.Base;

namespace Matrimony.Service.Contracts
{
    public interface IMasterDataService
    {
        Response GetMasterDate();
        Response GetCountry();
        Response GetState(int countryId);
        Response GetCity(int stateId);
    }
}
