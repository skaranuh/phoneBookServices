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
    {        private readonly IConfiguration _configuration;

        private readonly IDataTableConverter _dataTableConverter;

        public ExcelGenerator(IConfiguration configuration, IDataTableConverter dataTableConverter)
        {
            _configuration = configuration;
            _dataTableConverter = dataTableConverter;
        }
        public async Task<string> ExportToExcel(IEnumerable<ReportDto> reportData)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(_dataTableConverter.ToDataTable(reportData.ToList()), "report");
               
                var fileName = $"{Guid.NewGuid()}.xlsx";
                var filePath = $"./{_configuration["Report:Path"]}";
                
                var info = new DirectoryInfo(filePath);
                if (!info.Exists)
                {
                    info.Create();
                }

                var path = Path.Combine(filePath, fileName);

                wb.SaveAs(path);
                return await Task.FromResult(fileName);
            }
        }
    }
}