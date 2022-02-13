using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Common.Dtos;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IExcelGenerator
    {
        Task<string> ExportToExcel(IEnumerable<ReportDto> reportData);
    }
}