using PhoneBook.Api.Repositories.Entities.Base;

namespace PhoneBook.Api.Repositories.Entities
{
    public class ContactPerson : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}