using System.Collections.Generic;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactPersonDetailsDto
    {
        public ContactPersonDto ContactPerson { get; set; }
        public IEnumerable<ContactInfoDto> ContactInfo { get; set; }
    }
}