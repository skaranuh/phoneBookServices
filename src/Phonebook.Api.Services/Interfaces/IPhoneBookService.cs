using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.Services.Dtos;

namespace PhoneBook.Api.Services.Interfaces
{
    public interface IPhoneBookService
    {
        Task<Guid> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto);
        Task<Guid> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto);
        Task RemoveContactPerson(Guid contactInfoId);
        Task RemoveContactInfo(Guid contactInfoId);
        Task<IEnumerable<ContactPersonDto>> ListContactPersons();
    }
}