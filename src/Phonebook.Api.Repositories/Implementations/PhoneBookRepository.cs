using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Repositories.Interfaces;

namespace PhoneBook.Api.Repositories.Implementations
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private readonly PhoneBookDataContext _phoneBookDataContext;

        public PhoneBookRepository(PhoneBookDataContext phoneBookDataContext)
        {
            _phoneBookDataContext = phoneBookDataContext;
        }
        public Task<Guid> AddContactInfoToContactPerson(ContactInfo contactInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateContactPerson(ContactPerson contactPerson)
        {
            await _phoneBookDataContext.ContactPersons.AddAsync(contactPerson);
            await _phoneBookDataContext.SaveChangesAsync();
            return contactPerson.Id;
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