using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Services.Dtos;

namespace PhoneBook.Api.Services.Interfaces
{
    public interface IPhoneBookService
    {
        Task<ContactPersonDto> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto);
        Task<ContactInfoDto> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto);
        Task RemoveContactPerson(Guid contactInfoId);
        Task RemoveContactInfo(Guid contactInfoId);
        Task<IEnumerable<ContactPersonDto>> ListContactPersons();
        Task<ContactPersonDetailsDto> GetContactPersonDetails(Guid contactPersonId);
    }
}