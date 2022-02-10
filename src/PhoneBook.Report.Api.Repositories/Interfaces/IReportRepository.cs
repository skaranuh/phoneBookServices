using System.Threading.Tasks;
using PhoneBook.Report.Api.Entities.Entities;

namespace PhoneBook.Report.Api.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<ReportEntity> CreateReportRequest();
    }
}