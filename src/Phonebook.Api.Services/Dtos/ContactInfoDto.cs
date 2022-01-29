using System;
using PhoneBook.Api.Services.Enums;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactInfoDto
    {
        public Guid ContactInfoId { get; set; }
        public int ContactPersonId { get; set; }

        public ContactInfoType Type { get; set; }

        public string Value { get; set; }
    }
}