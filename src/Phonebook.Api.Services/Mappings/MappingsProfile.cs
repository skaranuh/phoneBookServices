using AutoMapper;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Services.Dtos;

namespace PhoneBook.Api.Services.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<ContactPersonCreateDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPersonCreateDto>();

            CreateMap<ContactPersonDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPersonDto>();

            CreateMap<ContactInfoAddDto, ContactInfo>().ForMember(x => x.Value, y => y.MapFrom(z => z.ContactInfo));
            CreateMap<ContactInfo, ContactInfoAddDto>().ForMember(x => x.ContactInfo, y => y.MapFrom(z => z.Value));

            CreateMap<ContactInfoDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoDto>();
        }
    }
}