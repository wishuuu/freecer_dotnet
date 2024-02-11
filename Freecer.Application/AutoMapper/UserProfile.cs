using AutoMapper;

namespace Freecer.Application.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Entities.User, Domain.Dtos.User.UserDto>();
    }
}