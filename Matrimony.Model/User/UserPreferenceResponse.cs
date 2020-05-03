using Matrimony.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserPreferenceResponse : SuccessResponse<UserPreferenceModel>
    {
        public UserPreferenceResponse(Metadata metadata, UserPreferenceModel userPrefModel)
            : base(metadata, userPrefModel)
        { }
    }
}
