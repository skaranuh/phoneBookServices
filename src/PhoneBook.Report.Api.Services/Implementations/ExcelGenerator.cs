using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class ExcelGenerator : IExcelGenerator
    {
        public Task<string> ExportToExcel(IEnumerable<ReportDto> reportData)
        {
            throw new System.NotImplementedException();
        }
    }
}