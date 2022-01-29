using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Phonebook.Api.Services.Dtos;
using Phonebook.Api.Services.Interfaces;
using PhoneBook.Api.Repositories.Entities;
using PhoneBook.Api.Repositories.Interfaces;

namespace PhoneBook.Api.Services.Implementations
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IPhoneBookRepository _phoneBookRepository;
        private readonly IMapper _map;
        public PhoneBookService(IPhoneBookRepository phoneBookRepository, IMapper map)
        {
            _phoneBookRepository = phoneBookRepository;
            _map = map;
        }
        public async Task<Guid> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto)
        {
            var contactInfo = _map.Map<ContactInfo>(contactInfoAddDto);
            var contactInfoId = await _phoneBookRepository.AddContactInfoToContactPerson(contactInfo);
            return contactInfoId;
        }

        public async Task<Guid> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto)
        {
            var contactPerson = _map.Map<ContactPerson>(contactPersonCreateDto);
            var contactPersonId = await _phoneBookRepository.CreateContactPerson(contactPerson);
            return contactPersonId;
        }

        public Task<ContactPersonDetailsDto> GetContactPersonDetails(Guid contactPersonId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactPersonDto>> ListContactPersons()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveContactInfo(Guid contactInfoId)
        {
           await _phoneBookRepository.RemoveContactInfo(contactInfoId);
        }

        public async Task RemoveContactPerson(Guid contactInfoId)
        {
            await _phoneBookRepository.RemoveContactPerson(contactInfoId);
        }
    }
}