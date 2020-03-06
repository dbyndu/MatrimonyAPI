using Matrimony.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserModelListResponse: SuccessResponse<List<UserModel>>
    {
        public UserModelListResponse(Metadata metadata, List<UserModel> userModel)
            : base(metadata, userModel)
        { }
    }
}
