using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneBook.Api.Entities.Base;

namespace PhoneBook.Api.Entities
{
    public class ContactPerson : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Company { get; set; }
        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
    }
}