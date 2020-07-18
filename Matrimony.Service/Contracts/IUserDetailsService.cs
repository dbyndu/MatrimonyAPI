﻿using System;
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
        Response CreateSocialUser(UserRegister user);
        Response CreateNewUser(UserShortRegister user);
        Response Register(Object obj, string type);
        Response GetImages(int userId, int width, int height, string mode);
        Response GestUserList(SearchCritriaModel searchCritria, string mode);
        Task<Response> SaveImage(UserImagesUploadModel userImages, int userId);
        Response GetUserDetails(int id);
        Response GetUserDetails(int userId, int viewedId);

        Response LoginUser(UserShortRegister user);

        Response LoginSocialUser(UserModel user);

        Response GetUserPreferences(int userId);
        Response GetProfileDisplayData(int userId);

        Response SaveChatInvite(int senderID, int receiverID);
        Task<Response> InterestOrShortListed(int userId, int interestUserId, string mode, int isRemoved, int isRejected);
        Task<Response> GetInterestShortListed(int id, int interestedId);
        Task<Response> GetNotificationData(int userId);
        Response UpdateNotification(int id);
        Response GenerateEmailCode(int userId);
        Response VerfiyEmailCode(int userId,string emailCode);
    }
}
