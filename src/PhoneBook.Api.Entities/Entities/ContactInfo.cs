using System;
using System.ComponentModel.DataAnnotations;
using PhoneBook.Api.Entities.Base;
using PhoneBook.Api.Entities.Enums;

namespace PhoneBook.Api.Entities
{
    public class ContactInfo : BaseEntity
    {
        [Required]
        public Guid ContactPersonId { get; set; }

        [Required]
        public ContactInfoType ContactInfoType { get; set; }
        [Required]
        [StringLength(100)]
        public string Value { get; set; }
    }
}