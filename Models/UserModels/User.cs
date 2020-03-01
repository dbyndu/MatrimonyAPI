using System;
using System.Collections.Generic;
using System.Text;

namespace Models.UserModels
{
    public class User : UserBasic
    {
        string Name { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string CreatedDate { get; set; }
        string ModifiedDate { get; set; }
    }
}
