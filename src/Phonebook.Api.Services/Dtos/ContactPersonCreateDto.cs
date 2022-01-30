using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Services.Dtos
{
    public class ContactPersonCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Company { get; set; }
    }
}
