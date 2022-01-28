using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Api.Controllers;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBook.Api.Tests
{
    public class PhoneBookControllerTests
    {
        [Fact]
        public async Task CreateContactPerson_Should_Create_ContactPerson()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactPersonCreateDto = new ContactPersonCreateDto { };
            var contactPersonId = Guid.NewGuid();
            phoneBookService.Setup(x => x.CreateContactPerson(contactPersonCreateDto)).ReturnsAsync(contactPersonId);

            //act
            var actionResult = await phoneBookController.CreateContactPerson(contactPersonCreateDto);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.CreateContactPerson(contactPersonCreateDto));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactPersonId, okObjectResult.Value);
        }

        [Fact]
        public async Task AddContactInfoToContactPerson_Should_Add_ContactInfo_To_ContactPerson()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactInfoAddDto = new ContactInfoAddDto { };
            var contactInfoId = Guid.NewGuid();
            phoneBookService.Setup(x => x.AddContactInfoToContactPerson(contactInfoAddDto)).ReturnsAsync(contactInfoId);

            //act
            var actionResult = await phoneBookController.AddContactInfoToContactPerson(contactInfoAddDto);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.AddContactInfoToContactPerson(contactInfoAddDto));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactInfoId, okObjectResult.Value);
        }

        [Fact]
        public async Task RemoveContactPerson_Should_Remove_Contact_Person()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactPersonId = Guid.NewGuid();

            //act
            var actionResult = await phoneBookController.RemoveContactPerson(contactPersonId);
            var okResult = actionResult as OkResult;

            //assert
            phoneBookService.Verify(x => x.RemoveContactPerson(contactPersonId));
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}