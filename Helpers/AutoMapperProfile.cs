namespace OperationsService.Helpers;

using AutoMapper;
using OperationsService.Entities;
using OperationsService.Models.Roles;
using OperationsService.Models.Users;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();
        CreateMap<CreateRole, Role>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
        CreateMap<User, UserRole>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));
    }
}