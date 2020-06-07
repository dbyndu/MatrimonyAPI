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

    public class UserImageListResponse : SuccessResponse<List<UserImage>>
    {
        public UserImageListResponse(Metadata metadata, List<UserImage> userImage)
            : base(metadata, userImage)
        { }
    }

    public class GenericOkResponse<T> : SuccessResponse<T>
    {
        public GenericOkResponse(Metadata metadata, T data)
            : base(metadata, data)
        { }
    }
}
