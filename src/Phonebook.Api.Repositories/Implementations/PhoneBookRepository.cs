using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PhoneBook.Api.Utilities.Exceptions;
using X.PagedList;

namespace PhoneBook.Api.Repositories.Implementations
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private readonly PhoneBookDataContext _phoneBookDataContext;

        public PhoneBookRepository(PhoneBookDataContext phoneBookDataContext)
        {
            _phoneBookDataContext = phoneBookDataContext;
        }
        public async Task<Guid> AddContactInfoToContactPerson(ContactInfo contactInfo)
        {
            var contactPerson = await _phoneBookDataContext.ContactPersons.FindAsync(contactInfo.ContactPersonId);
            if (contactPerson == null)
            {
                throw new NotFoundException($"Contact person not found: Contact person id : {contactInfo.ContactPersonId}");
            }
            if (contactPerson.ContactInfo == null)
            {
                contactPerson.ContactInfo = new List<ContactInfo>();
            }
            contactPerson.ContactInfo.Add(contactInfo);
            await _phoneBookDataContext.SaveChangesAsync();
            return contactInfo.Id;
        }

        public async Task<Guid> CreateContactPerson(ContactPerson contactPerson)
        {
            await _phoneBookDataContext.ContactPersons.AddAsync(contactPerson);
            await _phoneBookDataContext.SaveChangesAsync();
            return contactPerson.Id;
        }

        public async Task<ContactPerson> GetContactPersonDetails(Guid contactPersonId)
        {
            var contactPerson = await _phoneBookDataContext.ContactPersons.Include(x => x.ContactInfo).FirstOrDefaultAsync(x => x.Id == contactPersonId);
            if (contactPerson == null) { return null; }
            return contactPerson;
        }

        public async Task<IPagedList<ContactPerson>> ListContactPersons(int pageNumber, int pageSize)
        {
             var contactPersons = await _phoneBookDataContext.ContactPersons.AsQueryable().ToPagedListAsync(pageNumber, pageSize);
             return contactPersons;
        }

        public async Task RemoveContactInfo(Guid contactInfoId)
        {
            var contactInfo = _phoneBookDataContext.ContactInfos.FirstOrDefault(x => x.Id == contactInfoId);
            if (contactInfo == null)
            {
                throw new NotFoundException($"Contact info not found : {contactInfoId}");
            }
            _phoneBookDataContext.ContactInfos.Remove(contactInfo);
            await _phoneBookDataContext.SaveChangesAsync();
        }

        public async Task RemoveContactPerson(Guid contactPersonId)
        {
            var contactPerson = _phoneBookDataContext.ContactPersons.FirstOrDefault(x => x.Id == contactPersonId);
            if (contactPerson == null)
            {
                throw new NotFoundException($"Contact person not found : {contactPersonId}");
            }
            _phoneBookDataContext.ContactPersons.Remove(contactPerson);
            await _phoneBookDataContext.SaveChangesAsync();
        }
    }
}