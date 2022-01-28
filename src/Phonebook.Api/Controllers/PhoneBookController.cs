using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Services.Interfaces;

namespace PhoneBook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        public PhoneBookController(IPhoneBookService phoneBookService)
        {
            _phoneBookService = phoneBookService;
        }

        public async Task<IActionResult> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto)
        {
            var contactPersonId = await _phoneBookService.CreateContactPerson(contactPersonCreateDto);
            return Ok(contactPersonId);
        }

        public async Task<IActionResult>  AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto)
        {
            var contactInfoId=await _phoneBookService.AddContactInfoToContactPerson(contactInfoAddDto);
            return Ok(contactInfoId);
        }

        public async Task<IActionResult> RemoveContactPerson(Guid contactPersonId)
        {
            await _phoneBookService.RemoveContactPerson(contactPersonId);
            return Ok();           
        }

        public async Task<IActionResult>  RemoveContactInfo(Guid contactInfoId)
        {
            await _phoneBookService.RemoveContactInfo(contactInfoId);
            return Ok();
        }

        public async Task<IActionResult>  ListContactPersons()
        {
            var contactPersons=await _phoneBookService.ListContactPersons();
            return Ok(contactPersons);
        }
    }
}
