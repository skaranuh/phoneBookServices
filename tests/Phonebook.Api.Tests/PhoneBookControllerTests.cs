using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Controllers;
using PhoneBook.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using PhoneBook.Api.Entities.Entities;

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
            var contactPersonDto=new ContactPersonDto{Id=contactPersonId};
            phoneBookService.Setup(x => x.CreateContactPerson(contactPersonCreateDto)).ReturnsAsync(contactPersonDto);

            //act
            var actionResult = await phoneBookController.CreateContactPerson(contactPersonCreateDto);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.CreateContactPerson(contactPersonCreateDto));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactPersonId, ((ContactPersonDto)okObjectResult.Value).Id);
        }

        [Fact]
        public async Task AddContactInfoToContactPerson_Should_Add_ContactInfo_To_ContactPerson()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactInfoAddDto = new ContactInfoAddDto { };
            var contactInfoDto = new ContactInfoDto {} ;
            phoneBookService.Setup(x => x.AddContactInfoToContactPerson(contactInfoAddDto)).ReturnsAsync(contactInfoDto);

            //act
            var actionResult = await phoneBookController.AddContactInfoToContactPerson(contactInfoAddDto);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.AddContactInfoToContactPerson(contactInfoAddDto));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactInfoDto, okObjectResult.Value);
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

        [Fact]
        public async Task RemoveContactInfo_Should_Remove_ContactInfo_From_ContactPerson()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactInfoId = Guid.NewGuid();

            //act
            var actionResult = await phoneBookController.RemoveContactInfo(contactInfoId);
            var okResult = actionResult as OkResult;

            //assert
            phoneBookService.Verify(x => x.RemoveContactInfo(contactInfoId));
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ListContactPersons_Should_List_ContactPersons()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var contactPersons = new List<ContactPersonDto>();
            var contactPersonsCount = 10;
            for (var i = 0; i < contactPersonsCount; i++)
            {
                contactPersons.Add(new ContactPersonDto {Id = Guid.NewGuid(), Name = $"Name-{i}", LastName = $"LastName-{i}", Company = $"Company-{i}" });
            }

            phoneBookService.Setup(x => x.ListContactPersons()).ReturnsAsync(contactPersons);
            var phoneBookController = new PhoneBookController(phoneBookService.Object);

            //act
            var actionResult = await phoneBookController.ListContactPersons();
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.ListContactPersons());
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactPersons, okObjectResult.Value);
        }

        [Fact]
        public async Task GetContactPersonDetails_Should_Return_ContactPerson_Details()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactPersonId=Guid.NewGuid();
            var contactPersonDetailsDto=new ContactPersonDetailsDto{ContactPerson=new ContactPersonDto(), ContactInfo= new List<ContactInfoDto>()};
            phoneBookService.Setup(x=>x.GetContactPersonDetails(contactPersonId)).ReturnsAsync(contactPersonDetailsDto);

            //act
            var actionResult = await phoneBookController.GetContactPersonDetails(contactPersonId);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x=>x.GetContactPersonDetails(contactPersonId));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactPersonDetailsDto, okObjectResult.Value);
        }
    }
}