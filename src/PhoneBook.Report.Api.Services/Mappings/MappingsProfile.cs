using AutoMapper;
using PhoneBook.Report.Api.Entities.Entities;
using PhoneBook.Report.Api.Services.Dtos;

namespace PhoneBook.Report.Api.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<ReportEntity, ReportResponseDto>();
            CreateMap<ReportResponseDto, ReportEntity>();
        }
    }
}