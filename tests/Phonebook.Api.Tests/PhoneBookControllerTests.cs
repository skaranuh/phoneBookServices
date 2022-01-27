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
    }
}