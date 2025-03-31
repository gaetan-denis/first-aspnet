using AutoMapper;


namespace Api;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        //SOURCE -> DESTINATION
        _ = CreateMap<User, UserDto>();
        _ = CreateMap<Post, PostDto>();
        _ = CreateMap<Domain, DomainDto>();
    }
}