using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Report.Api.Services.Dtos;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportResponseDto> CreateReportRequest();
        Task<IEnumerable<ReportResponseDto>> ListReportRequests();
    }
}