using System;
using System.Threading.Tasks;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Common.Dtos;

namespace PhoneBook.Api.Services.Interfaces
{
    public interface IPhoneBookService
    {
        Task<ContactPersonDto> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto);
        Task<ContactInfoDto> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto);
        Task RemoveContactPerson(Guid contactInfoId);
        Task RemoveContactInfo(Guid contactInfoId);
        Task<PageListToSerialize<ContactPersonDto>> ListContactPersons(int pageNumber, int pageSize);
        Task<ContactPersonDto> GetContactPersonDetails(Guid contactPersonId);
        Task<PageListToSerialize<ReportDto>> GetReportData(int pageNumber, int pageSize);
    }
}