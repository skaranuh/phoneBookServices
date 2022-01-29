using PhoneBook.Api.Entities.Base;
using PhoneBook.Api.Entities.Enums;

namespace PhoneBook.Api.Entities
{
    public class ContactInfo : BaseEntity
    {
        public int ContactPersonId { get; set; }

        public ContactInfoType ContactInfoType { get; set; }

        public string Value { get; set; }
    }
}