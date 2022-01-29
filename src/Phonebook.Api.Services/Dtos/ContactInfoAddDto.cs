using PhoneBook.Api.Entities.Enums;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactInfoAddDto
    {
        public int ContactPersonId { get; set; }

        public ContactInfoType ContactInfoType { get; set; }

        public string ContactInfo { get; set; }
    }
}