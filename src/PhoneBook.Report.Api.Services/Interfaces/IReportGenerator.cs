using System;
using System.Threading.Tasks;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IReportGenerator
    {
        Task GenerateReport(Guid reportRequestId);
    }
}