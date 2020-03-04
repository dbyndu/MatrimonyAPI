using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.UserModels
{
    public class UserListResponse: SuccessResponse<List<User>>
    {
        public UserListResponse(Metadata metadata, List<User> userModel)
            : base(metadata, userModel)
        { }
    }
}
