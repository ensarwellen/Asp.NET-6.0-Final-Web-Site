using AutoMapper;
using BitirmeMovieStore.Entities;
using BitirmeMovieStore.Models;

namespace BitirmeMovieStore
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, EditUserModel>().ReverseMap();


        }
    }
}
