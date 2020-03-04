using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.UserModels
{
    class UserResponse: SuccessResponse<User>
    {
        public UserResponse(Metadata metadata, User userModel)
            :base(metadata, userModel)
        { }
    }
}
