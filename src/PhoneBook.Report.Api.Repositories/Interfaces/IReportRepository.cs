using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Report.Api.Entities.Entities;
using X.PagedList;

namespace PhoneBook.Report.Api.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<ReportEntity> CreateReportRequest();
        Task<IPagedList<ReportEntity>> ListReportRequests(int pageNumber, int pageSize);
        Task UpdateReportStatus(Guid reportRequestId, string reportPath);
    }
}