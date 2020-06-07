using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matrimony.Model.Base;
using Matrimony.Model.Common;
using Matrimony.Model.User;

namespace Matrimony.Service.Contracts
{
    public interface IUserDetailsService
    {
        Response GetUserDetails();
        Response GetOneUserDetails(string user);

        Response CreateNewUser(UserShortRegister user);
        Response Register(Object obj, string type);
        Response GetImages(int userId, int width, int height, string mode);
        Response GestUserList(SearchCritriaModel searchCritria);
        Task<Response> SaveImage(UserImagesUploadModel userImages, int userId);
        Response GetUserDetails(int id);

        Response LoginUser(UserShortRegister user);

        Response GetUserPreferences(int userId);
        Response GetProfileDisplayData(int userId);

        Response SaveChatInvite(int senderID, int receiverID);
    }
}
