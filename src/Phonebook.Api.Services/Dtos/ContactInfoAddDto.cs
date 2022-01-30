using System;
using System.ComponentModel.DataAnnotations;
using PhoneBook.Api.Entities;
using PhoneBook.Api.Entities.Enums;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactInfoAddDto
    {
        [Required]
        public Guid ContactPersonId { get; set; }

        [Required]
        public ContactInfoType ContactInfoType { get; set; }
        [Required]
        [StringLength(Constants.ContactInfoLength)]
        public string ContactInfo { get; set; }
    }
}