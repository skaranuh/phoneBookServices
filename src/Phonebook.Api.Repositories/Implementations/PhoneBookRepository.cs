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
using PhoneBook.Common.Dtos;

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

        public class ContactPersonPhoneNumber
        {
            public string PhoneNumber { get; set; }
            public Guid ContactPersonId { get; set; }
        }

        public class ContactPersonLocation
        {
            public string Location { get; set; }
            public Guid ContactPersonId { get; set; }
        }
        public class PhoneNumberLocation
        {
            public string Location { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class ContactPersonCountLocation
        {
            public string Location { get; set; }
            public int ContactPersonsCount { get; set; }
        }

        public class PhoneCountLocation
        {
            public string Location { get; set; }
            public int PhonesCount { get; set; }
        }

        public async Task<IPagedList<ReportDto>> GetReportData(int pageNumber, int pageSize)
        {
            var locationsOfContactPersons = from cp in _phoneBookDataContext.ContactPersons
                                            join ci in _phoneBookDataContext.ContactInfos
                                            on cp.Id equals ci.ContactPersonId
                                            where ci.ContactInfoType == Entities.Enums.ContactInfoType.Location
                                            select new ContactPersonLocation { Location = ci.Value, ContactPersonId = cp.Id };


            var contactPersonCountsByLocations = locationsOfContactPersons
            .GroupBy(x => x.Location).Select(g => new ContactPersonCountLocation { Location = g.Key, ContactPersonsCount = g.Count() }).ToList();

            var phoneNumbersOfContactPersons = from cp in _phoneBookDataContext.ContactPersons
                                               join ci in _phoneBookDataContext.ContactInfos
                                               on cp.Id equals ci.ContactPersonId
                                               where ci.ContactInfoType == Entities.Enums.ContactInfoType.Phone
                                               select new ContactPersonPhoneNumber { PhoneNumber = ci.Value, ContactPersonId = cp.Id };

            //since there is no relation between phone numbers and locations, if person has n locations and m phone numbers then
            // every phone number(m) will repeat in every location(n) for that contact person.  

            var phonesByLocations = from contactLocation in locationsOfContactPersons
                                    from contactPhone in phoneNumbersOfContactPersons
                                         .Where(p => p.ContactPersonId == contactLocation.ContactPersonId)
                                    select new PhoneNumberLocation { PhoneNumber = contactPhone.PhoneNumber, Location = contactLocation.Location };

            var phoneCountsByLocations = phonesByLocations.
                       GroupBy(x => x.Location).Select(g => new PhoneCountLocation { Location = g.Key, PhonesCount = g.Count() }).ToList ();


            var report = from contactPersonCountInLocation in contactPersonCountsByLocations
                         join phoneCountInLocation in phoneCountsByLocations
                         on contactPersonCountInLocation.Location equals phoneCountInLocation.Location into ps
                         from psInLocation in ps.DefaultIfEmpty()
                         select new ReportDto
                         {
                             Location = contactPersonCountInLocation == null ? null : contactPersonCountInLocation.Location,
                             PersonsCount = contactPersonCountInLocation == null ? 0 : contactPersonCountInLocation.ContactPersonsCount,
                             PhoneNumbersCount = psInLocation == null ? 0 : psInLocation.PhonesCount
                         };

            var result = await report.AsQueryable().ToPagedListAsync(pageNumber, pageSize);
            return result;

        }

        public async Task<IPagedList<ContactPerson>> ListContactPersons(int pageNumber, int pageSize)
        {
            var contactPersons = await _phoneBookDataContext.ContactPersons.OrderByDescending(x=>x.LastName).ThenBy(x=>x.Name).AsQueryable().ToPagedListAsync(pageNumber, pageSize);
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