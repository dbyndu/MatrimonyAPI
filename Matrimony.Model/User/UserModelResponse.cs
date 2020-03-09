using Matrimony.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserModelResponse: SuccessResponse<UserModel>
    {
        public UserModelResponse(Metadata metadata, UserModel userModel)
            :base(metadata, userModel)
        { }
    }
}
