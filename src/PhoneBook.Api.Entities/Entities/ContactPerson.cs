using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneBook.Api.Entities.Entities.Base;

namespace PhoneBook.Api.Entities.Entities
{
    public class ContactPerson : BaseEntity
    {
        [Required]
        [StringLength(Constants.NameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(Constants.LastNameLength)]
        public string LastName { get; set; }
        [Required]
        [StringLength(Constants.CompanyLength)]
        public string Company { get; set; }
        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
    }
}