using PhoneBook.Api.Repositories.Entities.Base;
using PhoneBook.Api.Repositories.Enums;

namespace PhoneBook.Api.Repositories.Entities
{
    public class ContactInfo : BaseEntity
    {
        public int ContactPersonId { get; set; }

        public ContactInfoType ContactInfoType { get; set; }

        public string Value { get; set; }
    }
}