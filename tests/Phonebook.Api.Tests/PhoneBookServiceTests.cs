using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Implementations;
using Xunit;
using System.Linq;
using PhoneBook.Api.Entities.Enums;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Api.Utilities.Exceptions;
using X.PagedList;
using PhoneBook.Common.Dtos;

namespace PhoneBook.Api.Tests
{
    public class PhoneBookServiceTests
    {
        [Theory]
        [MemberData(nameof(CreateContactPersonData))]
        public async Task CreateContactPerson_Should_Create_ContactPerson_Theory(string name, string lastName, string company)
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            var contactPersonCreateDto = new ContactPersonCreateDto { Name = name, LastName = lastName, Company = company };
            var contactPerson = new ContactPerson { Name = name, LastName = lastName, Company = company };
            var expectedContactPersonId = Guid.NewGuid();
            phoneBookRepository.Setup(x => x.CreateContactPerson(It.IsAny<ContactPerson>())).ReturnsAsync(expectedContactPersonId);
            var contactPersonDtoExpected = new ContactPersonDto { Id = expectedContactPersonId, Name = name, LastName = lastName, Company = company };
            mapper.Setup(x => x.Map<ContactPerson>(contactPersonCreateDto)).Returns(contactPerson);
            mapper.Setup(x => x.Map<ContactPersonDto>(contactPerson)).Returns(contactPersonDtoExpected);

            //act
            var contactPersonDtoResult = await phoneBookService.CreateContactPerson(contactPersonCreateDto);

            //assert
            phoneBookRepository.Verify(x => x.CreateContactPerson(It.Is<ContactPerson>(x => x.Name == name && x.LastName == lastName && x.Company == company)));
            Assert.Equal(contactPersonDtoExpected.Id, contactPersonDtoResult.Id);
        }

        [Fact]
        public async Task AddContactInfoToContactPerson_Should_Add_ContactInfo_To_ContactPerson()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var contactInfo = new ContactInfo { };
            var expectedContactInfoDto = new ContactInfoDto { ContactPersonId = Guid.NewGuid() };
            mapper.Setup(x => x.Map<ContactInfoDto>(contactInfo)).Returns(expectedContactInfoDto);

            var contactInfoAddDto = new ContactInfoAddDto { };
            mapper.Setup(x => x.Map<ContactInfo>(contactInfoAddDto)).Returns(contactInfo);
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            //act
            var contactInfoDto = await phoneBookService.AddContactInfoToContactPerson(contactInfoAddDto);

            //assert
            phoneBookRepository.Verify(x => x.AddContactInfoToContactPerson(contactInfo));
            mapper.Verify(x => x.Map<ContactInfo>(contactInfoAddDto));
            Assert.Equal(expectedContactInfoDto, contactInfoDto);
        }

        [Fact]
        public async Task AddContactInfoToContactPerson_Should_Throw_Exception_When_ContactPerson_Is_Null()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var contactAddInfo = new ContactInfoAddDto { };
            var contactInfo = new ContactInfo { };
            var notFoundException = new NotFoundException("Contact person not found");
            phoneBookRepository.Setup(x => x.AddContactInfoToContactPerson(contactInfo)).ThrowsAsync(notFoundException);
            mapper.Setup(x => x.Map<ContactInfo>(contactAddInfo)).Returns(contactInfo);

            //act           
            Func<Task> act = () => phoneBookService.AddContactInfoToContactPerson(contactAddInfo);

            //assert            
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(Utilities.ErrorCodes.NotFound, exception.ErrorCode);
        }

        [Fact]
        public async Task RemoveContactPerson_Should_Remove_Contact_Person()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var contactPersonId = Guid.NewGuid();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            //act
            await phoneBookService.RemoveContactPerson(contactPersonId);

            //assert
            phoneBookRepository.Verify(x => x.RemoveContactPerson(contactPersonId));
        }

        [Fact]
        public async Task RemoveContactPerson__Should_Throw_Exception_When_Repository_Method_Throws_Exception()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var contactPersonId = Guid.NewGuid();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var notFoundException = new NotFoundException("Contact person not found");
            phoneBookRepository.Setup(x => x.RemoveContactPerson(contactPersonId)).ThrowsAsync(notFoundException);

            //act           
            Func<Task> act = () => phoneBookService.RemoveContactPerson(contactPersonId);

            //assert            
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(Utilities.ErrorCodes.NotFound, exception.ErrorCode);

        }

        [Fact]
        public async Task RemoveContactInfo_Should_Remove_ContactInfo_From_ContactPerson()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var contactInfoId = Guid.NewGuid();

            //act
            await phoneBookService.RemoveContactInfo(contactInfoId);

            //assert
            phoneBookRepository.Verify(x => x.RemoveContactInfo(contactInfoId));
        }

        [Fact]
        public async Task RemoveContactInfo_Should_Throw_Exception_When_Repository_Method_Throws_Exception()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var contactInfoId = Guid.NewGuid();
            var notFoundException = new NotFoundException("Contact person not found");
            phoneBookRepository.Setup(x => x.RemoveContactInfo(contactInfoId)).ThrowsAsync(notFoundException);
            //act           
            Func<Task> act = () => phoneBookService.RemoveContactInfo(contactInfoId);

            //assert            
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(Utilities.ErrorCodes.NotFound, exception.ErrorCode);
        }

        [Fact]
        public async Task ListContactPersons_Should_List_ContactPersons()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();

            var contactPersons = new List<ContactPerson>();
            var contactPersonDtos = new List<ContactPersonDto>();

            var contactPersonsCount = 10;
            for (var i = 0; i < contactPersonsCount; i++)
            {
                var id = Guid.NewGuid();
                contactPersons.Add(new ContactPerson { Id = id, Name = $"Name-{i}", LastName = $"LastName-{i}", Company = $"Company-{i}" });
                contactPersonDtos.Add(new ContactPersonDto { Id = id, Name = $"Name-{i}", LastName = $"LastName-{i}", Company = $"Company-{i}" });
            }

            var pageNumber = 1;
            var pageSize = 1;
            var contactPersonsPaged = new PagedList<ContactPerson>(contactPersons.AsQueryable(), pageNumber, pageSize);

            var contactPersonPagedToSerialize = new PageListToSerialize<ContactPersonDto>
            {
                List = contactPersonDtos,
                MetaData = contactPersonsPaged.GetMetaData()
            };

            phoneBookRepository.Setup(x => x.ListContactPersons(pageNumber, pageSize)).ReturnsAsync(contactPersonsPaged);
            mapper.Setup(x => x.Map<IEnumerable<ContactPersonDto>>(contactPersonsPaged)).Returns(contactPersonDtos);
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            //act
            var actualContactPersonDtos = await phoneBookService.ListContactPersons(pageNumber, pageSize);

            //assert
            phoneBookRepository.Verify(x => x.ListContactPersons(pageNumber, pageSize));
            Assert.Equal(contactPersonPagedToSerialize.List.Count(), actualContactPersonDtos.List.Count());
        }

        [Fact]
        public async Task GetContactPersonDetails_Should_Return_ContactPerson_Details()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var contactPersonId = Guid.NewGuid();
            var contactPerson = new ContactPerson { ContactInfo = new List<ContactInfo> { new ContactInfo { ContactInfoType = ContactInfoType.Email, Value = "dummy@Email.com" } }, Name = "dummyName", LastName = "dummyLastName", Company = "dummyCompany" };
            var contactPersonDto = new ContactPersonDto { ContactInfo = new List<ContactInfo>(), Name = contactPerson.Name, LastName = contactPerson.LastName, Company = contactPerson.Company };

            mapper.Setup(x => x.Map<ContactPersonDto>(contactPerson)).Returns(contactPersonDto);

            phoneBookRepository.Setup(x => x.GetContactPersonDetails(contactPersonId)).ReturnsAsync(contactPerson);

            //act
            var contactPersonDetails = await phoneBookService.GetContactPersonDetails(contactPersonId);

            //assert
            phoneBookRepository.Verify(x => x.GetContactPersonDetails(contactPersonId));
            Assert.Equal(contactPersonDto.Name, contactPersonDetails.Name);
            Assert.Equal(contactPersonDto.LastName, contactPersonDetails.LastName);
            Assert.Equal(contactPersonDto.Company, contactPersonDetails.Company);
            foreach (var contactInfo in contactPersonDetails.ContactInfo)
            {
                Assert.NotNull(contactPersonDto.ContactInfo.Where(x => x.Value == contactInfo.Value));
            }
        }

        [Fact]
        public async Task GetContactPersonDetails_Should_Throw_Exception_When_ContactPerson_Is_Null()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var contactPersonId = Guid.NewGuid();
            ContactPerson contactPerson = null;

            phoneBookRepository.Setup(x => x.GetContactPersonDetails(contactPersonId)).ReturnsAsync(contactPerson);

            //act           
            Func<Task> act = () => phoneBookService.GetContactPersonDetails(contactPersonId);

            //assert            
            var exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(Utilities.ErrorCodes.NotFound, exception.ErrorCode);
            phoneBookRepository.Verify(x => x.GetContactPersonDetails(contactPersonId));
        }


        [Fact]
        public async Task GetReport_Should_Return_Report_Data()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);
            var pageNumber = 1;
            var pageSize = 1;
          
            var reportDtos = new List<ReportDto>();

            var reportDtosCount = 10;
            for (var i = 0; i < reportDtosCount; i++)
            {
                var id = Guid.NewGuid();
                reportDtos.Add(new ReportDto { Location = $"location-{i}", PersonsCount = 1, PhoneNumbersCount = 2 });
            }

            var reportPaged = new PagedList<ReportDto>(reportDtos, pageNumber, pageSize);

            var reportPagedToSerialize = new PageListToSerialize<ReportDto>
            {
                List = reportDtos,
                MetaData = reportPaged.GetMetaData()
            };

            phoneBookRepository.Setup(x => x.GetReportData(pageNumber, pageSize)).ReturnsAsync(reportPaged);
            mapper.Setup(x => x.Map<IEnumerable<ReportDto>>(reportPaged)).Returns(reportDtos);

            //act
            var reportData = await phoneBookService.GetReportData(pageNumber, pageSize);

            //assert

            phoneBookRepository.Verify(x => x.GetReportData(pageNumber, pageSize));

            Assert.Equal(reportPagedToSerialize.List.Count(), reportData.List.Count());
        }

        public static IEnumerable<object[]> CreateContactPersonData
        {
            get
            {
                var _1Character = "1";
                var _100CharacterString = "100-character-string-abc??defg??h??ijklmno??prs??tu??vy-ABC??DEFG??HIIJKLMNO??PRS??TU??VY-0123456789-0123456789";

                yield return new object[] { _1Character, _1Character, _1Character };
                yield return new object[] { _100CharacterString, _100CharacterString, _100CharacterString };

            }
        }
    }
}