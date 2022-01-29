using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Phonebook.Api.Services.Dtos;
using PhoneBook.Api.Repositories.Entities;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Implementations;
using Xunit;

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

        public static IEnumerable<object[]> CreateContactPersonData
        {
            get
            {
                var _1Character="1";
                var _100CharacterString= "100-character-string-abcçdefgğhıijklmnoöprsştuüvy-ABCÇDEFGĞHIIJKLMNOÖPRSŞTUÜVY-0123456789-0123456789";                
                
                yield return new object[] { _1Character, _1Character, _1Character };
                yield return new object[] { _100CharacterString, _100CharacterString, _100CharacterString };
                
            }
        }
    }
}