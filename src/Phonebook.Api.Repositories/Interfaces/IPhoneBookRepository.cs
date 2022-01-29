using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.Repositories.Entities;

namespace PhoneBook.Api.Repositories.Interfaces
{
    public interface IPhoneBookRepository
    {
        Task<Guid> CreateContactPerson(ContactPerson contactPerson);
        Task<Guid> AddContactInfoToContactPerson(ContactInfo contactInfo);
        Task RemoveContactPerson(Guid contactPersonId);
        Task RemoveContactInfo(Guid contactInfoId);
        Task<IEnumerable<ContactPerson>> ListContactPersons();
    }
}