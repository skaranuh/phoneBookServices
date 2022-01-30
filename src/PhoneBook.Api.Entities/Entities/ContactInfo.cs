using System;
using System.ComponentModel.DataAnnotations;
using PhoneBook.Api.Entities.Entities.Base;
using PhoneBook.Api.Entities.Enums;

namespace PhoneBook.Api.Entities.Entities
{
    public class ContactInfo : BaseEntity
    {
        [Required]
        public Guid ContactPersonId { get; set; }

        [Required]
        public ContactInfoType ContactInfoType { get; set; }
        [Required]
        [StringLength(Constants.ContactInfoLength)]
        public string Value { get; set; }
    }
}