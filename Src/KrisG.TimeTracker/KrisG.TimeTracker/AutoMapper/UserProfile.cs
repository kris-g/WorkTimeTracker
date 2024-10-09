using AutoMapper;
using KrisG.TimeTracker.Entities;
using KrisG.TimeTracker.Models.Users;

namespace KrisG.TimeTracker.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<User, AuthenticatedUserModel>();
        }
    }
}
