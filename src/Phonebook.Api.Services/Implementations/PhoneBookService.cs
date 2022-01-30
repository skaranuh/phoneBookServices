using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Interfaces;
using PhoneBook.Api.Utilities.Exceptions;

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

        public async Task<ContactPersonDetailsDto> GetContactPersonDetails(Guid contactPersonId)
        {
           var contactPersonDetails= await _phoneBookRepository.GetContactPersonDetails(contactPersonId);       
           if (contactPersonDetails==null)
           {
               throw new NotFoundException($"Contact person not found: Contact person id : {contactPersonId}");
           }  
           var contactPersonDto=_map.Map<ContactPersonDto>(contactPersonDetails);
           var contactInfoDto=_map.Map<IEnumerable<ContactInfoDto>>(contactPersonDetails.ContactInfo);
           var contactPersonDetailsDto=new ContactPersonDetailsDto{ContactPerson=contactPersonDto, ContactInfo=contactInfoDto};
           return contactPersonDetailsDto;
        }

        public async Task<IEnumerable<ContactPersonDto>> ListContactPersons()
        {
             var contactPersons= await _phoneBookRepository.ListContactPersons();
             var contactPersonDtos = _map.Map<IEnumerable<ContactPersonDto>>(contactPersons);
             return contactPersonDtos;
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