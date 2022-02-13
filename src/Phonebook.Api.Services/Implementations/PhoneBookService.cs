using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Interfaces;
using PhoneBook.Api.Utilities.Exceptions;
using PhoneBook.Common.Dtos;

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
        public async Task<ContactInfoDto> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto)
        {
            var contactInfo = _map.Map<ContactInfo>(contactInfoAddDto);
            var contactInfoId = await _phoneBookRepository.AddContactInfoToContactPerson(contactInfo);
            var contactInfoDto = _map.Map<ContactInfoDto>(contactInfo);
            return contactInfoDto;
        }

        public async Task<ContactPersonDto> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto)
        {
            var contactPerson = _map.Map<ContactPerson>(contactPersonCreateDto);
            var contactPersonId = await _phoneBookRepository.CreateContactPerson(contactPerson);
            var contactPersonDto = _map.Map<ContactPersonDto>(contactPerson);
            return contactPersonDto;
        }

        public async Task<ContactPersonDto> GetContactPersonDetails(Guid contactPersonId)
        {
            var contactPersonDetails = await _phoneBookRepository.GetContactPersonDetails(contactPersonId);
            if (contactPersonDetails == null)
            {
                throw new NotFoundException($"Contact person not found: Contact person id : {contactPersonId}");
            }
            var contactPersonDto = _map.Map<ContactPersonDto>(contactPersonDetails);
            return contactPersonDto;
        }

        public async Task<PageListToSerialize<ReportDto>> GetReportData(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            { pageNumber = 1; }

            if (pageSize == 0)
            { pageSize = 100; }

            var report = await _phoneBookRepository.GetReportData(pageNumber, pageSize);
            var reportDtos = _map.Map<IEnumerable<ReportDto>>(report);
            var pageListToSerialize = new PageListToSerialize<ReportDto>
            {
                List = reportDtos,
                MetaData = report.GetMetaData()
            };

            return pageListToSerialize;
        }

        public async Task<PageListToSerialize<ContactPersonDto>> ListContactPersons(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            { pageNumber = 1; }

            if (pageSize == 0)
            { pageSize = 100; }

            var contactPersons = await _phoneBookRepository.ListContactPersons(pageNumber, pageSize);
            var contactPersonDtos = _map.Map<IEnumerable<ContactPersonDto>>(contactPersons);
            var pageListToSerialize = new PageListToSerialize<ContactPersonDto>
            {
                List = contactPersonDtos,
                MetaData = contactPersons.GetMetaData()
            };

            return pageListToSerialize;
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