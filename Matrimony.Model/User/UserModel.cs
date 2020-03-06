using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserModel : UserBasicModel
    {
        string Name { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string CreatedDate { get; set; }
        string ModifiedDate { get; set; }
    }
}
