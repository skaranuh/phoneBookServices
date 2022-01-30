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
using X.PagedList;
using System.Linq;
using System.Text.Json;
using PhoneBook.Api.Services;

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
            var contactPersonDto = new ContactPersonDto { Id = contactPersonId };
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
            var contactInfoDto = new ContactInfoDto { };
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
                contactPersons.Add(new ContactPersonDto { Id = Guid.NewGuid(), Name = $"Name-{i}", LastName = $"LastName-{i}", Company = $"Company-{i}" });
            }

            var pageNumber = 1;
            var pageSize = 1;
            var contactPersonsPaged = new PagedList<ContactPersonDto>(contactPersons.AsQueryable(), pageNumber, pageSize);

            var serializedContactPersonPagedDtos = new PageListToSerialize<ContactPersonDto>
            {
                List = contactPersonsPaged,
                MetaData = contactPersonsPaged.GetMetaData()
            };

            phoneBookService.Setup(x => x.ListContactPersons(pageNumber, pageSize)).ReturnsAsync(serializedContactPersonPagedDtos);
            var phoneBookController = new PhoneBookController(phoneBookService.Object);

            //act
             var actionResult = await phoneBookController.ListContactPersons(pageNumber, pageSize);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.ListContactPersons(pageNumber, pageSize));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(serializedContactPersonPagedDtos, okObjectResult.Value);
        }

        [Fact]
        public async Task GetContactPersonDetails_Should_Return_ContactPerson_Details()
        {
            //arrange
            var phoneBookService = new Mock<IPhoneBookService>();
            var phoneBookController = new PhoneBookController(phoneBookService.Object);
            var contactPersonId = Guid.NewGuid();
            var contactPersonDetailsDto = new ContactPersonDto { };
            phoneBookService.Setup(x => x.GetContactPersonDetails(contactPersonId)).ReturnsAsync(contactPersonDetailsDto);

            //act
            var actionResult = await phoneBookController.GetContactPersonDetails(contactPersonId);
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            phoneBookService.Verify(x => x.GetContactPersonDetails(contactPersonId));
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(contactPersonDetailsDto, okObjectResult.Value);
        }
    }
}