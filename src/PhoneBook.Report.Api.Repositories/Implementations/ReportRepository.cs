using System.Threading.Tasks;
using PhoneBook.Report.Api.Entities.Entities;
using PhoneBook.Report.Api.Repositories.Interfaces;
using X.PagedList;
using PhoneBook.Report.Api.DataContext;
using System;

namespace PhoneBook.Report.Api.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly PhoneBookReportDataContext _phoneBookReportDataContext;
        public ReportRepository(PhoneBookReportDataContext phoneBookReportDataContext)
        {
            _phoneBookReportDataContext = phoneBookReportDataContext;
        }
        public async Task<ReportEntity> CreateReportRequest()
        {
            var reportEntity = new ReportEntity { RequestDate = DateTime.Now, Status = Entities.Enums.ReportStatus.Pending };
            await _phoneBookReportDataContext.AddAsync(reportEntity);
            await _phoneBookReportDataContext.SaveChangesAsync();
            return reportEntity;
        }

        public async Task<IPagedList<ReportEntity>> ListReportRequests(int pageNumber, int pageSize)
        {
            var reports = await _phoneBookReportDataContext.Reports.AsQueryable().ToPagedListAsync(pageNumber, pageSize);
            return reports;
        }
    }
}