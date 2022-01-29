using System.Collections.Generic;
using PhoneBook.Api.Services.Dtos;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactPersonDetailsDto
    {
        public ContactPersonDto ContactPerson { get; set; }
        public IEnumerable<ContactInfoDto> ContactInfo { get; set; }
    }
}