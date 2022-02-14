using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class ExcelGenerator : IExcelGenerator
    {
        private readonly IFileOperations _fileOperations;
        private readonly IConfiguration _configuration;
        public ExcelGenerator(IFileOperations fileOperations, IConfiguration configuration)
        {
            _fileOperations = fileOperations;
            _configuration = configuration;
        }
        public async Task<string> ExportToExcel(IEnumerable<ReportDto> reportData)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DataTableConverter.ToDataTable(reportData.ToList()));
                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    var fileName = $"{Guid.NewGuid()}.xlsx";
                    var filePath = _configuration["Report:Path"];
                    await _fileOperations.SaveStreamAsFile(filePath, stream, fileName);
                    return fileName;
                }
            }
        }
    }
}