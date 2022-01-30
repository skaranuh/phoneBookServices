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
        }
    }
}