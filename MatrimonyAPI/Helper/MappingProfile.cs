using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Matrimony.Model.User;

namespace MatrimonyAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Matrimony.Data.Entities.User, UserModel>().ForMember(x => x.Password, opt => opt.Ignore());
            CreateMap<Matrimony.Data.Entities.UserInfo, UserFamilyInformationModel>();
            //CreateMap<UserDto, User>();
        }
    }
}
