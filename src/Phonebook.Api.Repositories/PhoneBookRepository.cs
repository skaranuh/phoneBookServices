using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Repositories.Interfaces;

namespace PhoneBook.Api.Repositories
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        public Task<Guid> AddContactInfoToContactPerson(ContactInfo contactInfo)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateContactPerson(ContactPerson contactPerson)
        {
            throw new NotImplementedException();
        }

        public Task<ContactPerson> GetContactPersonDetails(Guid contactPersonId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactPerson>> ListContactPersons()
        {
            throw new NotImplementedException();
        }

        public Task RemoveContactInfo(Guid contactInfoId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveContactPerson(Guid contactPersonId)
        {
            throw new NotImplementedException();
        }
    }
}