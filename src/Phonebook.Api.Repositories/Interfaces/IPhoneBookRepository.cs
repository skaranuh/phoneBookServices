using System;
using System.Threading.Tasks;
using PhoneBook.Api.Entities.Entities;
using PhoneBook.Common.Dtos;
using X.PagedList;

namespace PhoneBook.Api.Repositories.Interfaces
{
    public interface IPhoneBookRepository
    {
        Task<Guid> CreateContactPerson(ContactPerson contactPerson);
        Task<Guid> AddContactInfoToContactPerson(ContactInfo contactInfo);
        Task RemoveContactPerson(Guid contactPersonId);
        Task RemoveContactInfo(Guid contactInfoId);
        Task<IPagedList<ContactPerson>> ListContactPersons(int pageNumber, int pageSize);
        Task<ContactPerson> GetContactPersonDetails(Guid contactPersonId);
        Task<IPagedList<ReportDto>> GetReportData(int pageNumber,int pageSize);
    }
}