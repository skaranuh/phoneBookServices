using System;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactPersonDto
    {
        public Guid ContactPersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}