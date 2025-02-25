namespace eCommerce.Users.Core.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, AuthResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<RegisterRequest, AppUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<AppUser, UserDto>()
            .ForMember(des => des.UserId, opt
                => opt.MapFrom(src => src.UserId))
            .ForMember(des => des.Email, opt
                => opt.MapFrom(src => src.Email))
            .ForMember(des => des.PersonName, opt
                => opt.MapFrom(src => src.PersonName))
            .ForMember(des => des.Gender, opt
                => opt.MapFrom(src => src.Gender));
    }
}