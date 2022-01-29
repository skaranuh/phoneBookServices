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
using PhoneBook.Api.Entities;
using PhoneBook.Api.Entities.Enums;

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
            mapper.Setup(x => x.Map<ContactPerson>(contactPersonCreateDto)).Returns(contactPerson);

            //act
            var contactPersonId = await phoneBookService.CreateContactPerson(contactPersonCreateDto);

            //assert
            phoneBookRepository.Verify(x => x.CreateContactPerson(It.Is<ContactPerson>(x => x.Name == name && x.LastName == lastName && x.Company == company)));
            Assert.Equal(expectedContactPersonId, contactPersonId);
        }

        [Fact]
        public async Task AddContactInfoToContactPerson_Should_Add_ContactInfo_To_ContactPerson()
        {
            //arrange
            var phoneBookRepository = new Mock<IPhoneBookRepository>();
            var mapper = new Mock<IMapper>();
            var contactInfo = new ContactInfo { };
            var expectedContactInfoId = Guid.NewGuid();
            phoneBookRepository.Setup(x => x.AddContactInfoToContactPerson(contactInfo)).ReturnsAsync(expectedContactInfoId);

            var contactInfoAddDto = new ContactInfoAddDto { };
            mapper.Setup(x => x.Map<ContactInfo>(contactInfoAddDto)).Returns(contactInfo);
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            //act
            var contactInfoId = await phoneBookService.AddContactInfoToContactPerson(contactInfoAddDto);

            //assert
            phoneBookRepository.Verify(x => x.AddContactInfoToContactPerson(contactInfo));
            mapper.Verify(x => x.Map<ContactInfo>(contactInfoAddDto));
            Assert.Equal(expectedContactInfoId, contactInfoId);
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

            phoneBookRepository.Setup(x => x.ListContactPersons()).ReturnsAsync(contactPersons);
            mapper.Setup(x => x.Map<IEnumerable<ContactPersonDto>>(contactPersons)).Returns(contactPersonDtos);
            var phoneBookService = new PhoneBookService(phoneBookRepository.Object, mapper.Object);

            //act
            var actualContactPersonDtos = await phoneBookService.ListContactPersons();

            //assert
            phoneBookRepository.Verify(x => x.ListContactPersons());
            mapper.Verify(x => x.Map<IEnumerable<ContactPersonDto>>(contactPersons));
            Assert.Equal(contactPersonDtos, actualContactPersonDtos);
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
            var contactPersonDto = new ContactPersonDto { ContactInfo=new List<ContactInfo>(),  Name = contactPerson.Name, LastName = contactPerson.LastName, Company = contactPerson.Company };

            mapper.Setup(x => x.Map<ContactPersonDto>(contactPerson)).Returns(contactPersonDto);

            phoneBookRepository.Setup(x => x.GetContactPersonDetails(contactPersonId)).ReturnsAsync(contactPerson);

            //act
            var contactPersonDetails = await phoneBookService.GetContactPersonDetails(contactPersonId);

            //assert
            phoneBookRepository.Verify(x => x.GetContactPersonDetails(contactPersonId));
            Assert.Equal(contactPersonDto.Name, contactPersonDetails.ContactPerson.Name);
            Assert.Equal(contactPersonDto.LastName, contactPersonDetails.ContactPerson.LastName);
            Assert.Equal(contactPersonDto.Company, contactPersonDetails.ContactPerson.Company);
            foreach (var contactInfo in contactPersonDetails.ContactInfo)
            {
               Assert.NotNull(contactPersonDto.ContactInfo.Where(x=>x.Value==contactInfo.Value));
            }         
        }

        public static IEnumerable<object[]> CreateContactPersonData
        {
            get
            {
                var _1Character = "1";
                var _100CharacterString = "100-character-string-abcçdefgğhıijklmnoöprsştuüvy-ABCÇDEFGĞHIIJKLMNOÖPRSŞTUÜVY-0123456789-0123456789";

                yield return new object[] { _1Character, _1Character, _1Character };
                yield return new object[] { _100CharacterString, _100CharacterString, _100CharacterString };

            }
        }
    }
}