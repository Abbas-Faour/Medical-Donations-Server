using API.DTOs;
using API.DTOs.Auth;
using AutoMapper;
using Core.Entites;
using Core.Entites.Identity;
using Core.Entites.JWT;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entites -> DTOs
            CreateMap<Category, KeyValuePairDto>();

            CreateMap<Medicine, MedicineDto>()

                .ForMember(m => m.User, opt => opt.MapFrom(src => src.ApplicationUser))

                .ForMember(m => m.Category, opt => opt.MapFrom(src => new KeyValuePairDto { Id = src.Category.Id, Name = src.Category.Name }));

            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));

            CreateMap<ApplicationUser, ApplicationUserDto>();

            // Dtos -> Entites
            CreateMap<QueryDto, Query>();

            CreateMap<UserLoginDto, LoginModel>();
            CreateMap<UserRegisterDto, RegisterModel>();

            CreateMap<MedicineToAddDto, Medicine>()
            .ForMember(m => m.ApplicationUserId, opt => opt.Ignore())
            .ForMember(m => m.ApplicationUser, opt => opt.Ignore());

            CreateMap<AddressToAddDto, Address>();
            CreateMap<FeedToAddDto, Feedback>();

        }
    }
}