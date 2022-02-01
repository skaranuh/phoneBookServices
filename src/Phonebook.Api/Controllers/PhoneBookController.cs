using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Services.Dtos;
using PhoneBook.Api.Services.Interfaces;

namespace PhoneBook.Api.Controllers
{
    [ApiController]
    [Route("api/phoneBook")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        public PhoneBookController(IPhoneBookService phoneBookService)
        {
            _phoneBookService = phoneBookService;
        }

        [HttpPost]
        [Route("contactPersons")]
        public async Task<IActionResult> CreateContactPerson(ContactPersonCreateDto contactPersonCreateDto)
        {
            var contactPersonId = await _phoneBookService.CreateContactPerson(contactPersonCreateDto);
            return Ok(contactPersonId);
        }

        [HttpPost]
        [Route("contactInfos")]
        public async Task<IActionResult> AddContactInfoToContactPerson(ContactInfoAddDto contactInfoAddDto)
        {
            var contactInfoId = await _phoneBookService.AddContactInfoToContactPerson(contactInfoAddDto);
            return Ok(contactInfoId);
        }

        [HttpDelete]
        [Route("contactPersons/{contactPersonId:guid}")]
        public async Task<IActionResult> RemoveContactPerson(Guid contactPersonId)
        {
            await _phoneBookService.RemoveContactPerson(contactPersonId);
            return Ok();
        }

        [HttpDelete]
        [Route("contactInfos/{contactInfoId:guid}")]
        public async Task<IActionResult> RemoveContactInfo(Guid contactInfoId)
        {
            await _phoneBookService.RemoveContactInfo(contactInfoId);
            return Ok();
        }

        [HttpGet]
        [Route("contactPersons")]
        public async Task<IActionResult> ListContactPersons(int pageNumber, int pageSize)
        {
            var contactPersons = await _phoneBookService.ListContactPersons(pageNumber, pageSize);
            return Ok(contactPersons);
        }

        [HttpGet]
        [Route("contactPersons/{contactPersonId:guid}")]
        public async Task<IActionResult> GetContactPersonDetails(Guid contactPersonId)
        {
            var contactPersonDetails = await _phoneBookService.GetContactPersonDetails(contactPersonId);
            return Ok(contactPersonDetails);
        }

        [HttpGet]
        [Route("report")]
        public async Task<IActionResult> GetReportData(int pageNumber, int pageSize)
        {
            var report = await  _phoneBookService.GetReportData(pageNumber, pageSize);
            return Ok(report);
        }
    }
}
