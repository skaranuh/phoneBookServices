using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Services.Interfaces;
using System.Linq;
using PhoneBook.Common.Dtos;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly IReportRepository _reportRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebServiceRequest _webServiceRequest;
        private readonly IExcelGenerator _excelGenerator;
        public ReportGenerator(IReportRepository reportRepository,
        IConfiguration configuration,
        IWebServiceRequest webServiceRequest,
        IExcelGenerator excelGenerator)
        {
            _reportRepository = reportRepository;
            _configuration = configuration;
            _webServiceRequest = webServiceRequest;
            _excelGenerator = excelGenerator;
        }

        public async Task GenerateReport(Guid reportRequestId)
        {
            var allReportData = new List<ReportDto>();
            int.TryParse(_configuration["Report:PageSize"], out int pageSize);
            if (pageSize == 0)
            {
                pageSize = 100;
            }

            var initialPage = 1;
            var reportData = await _webServiceRequest.GetReportData(initialPage, pageSize);
            allReportData.AddRange(reportData.List.Values);

            for (int i = initialPage + 1; i <= reportData.MetaData.PageCount; i++)
            {
                reportData = await _webServiceRequest.GetReportData(i, pageSize);
                allReportData.AddRange(reportData.List.Values);
            }

            var excelPath = await _excelGenerator.ExportToExcel(allReportData);
            await _reportRepository.UpdateReportStatus(reportRequestId, excelPath);
        }
    }
}