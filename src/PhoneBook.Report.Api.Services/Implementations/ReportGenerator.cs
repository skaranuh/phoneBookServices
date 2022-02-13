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
        private readonly IConfiguration _config;
        private readonly IWebServiceRequest _webServiceRequest;
        private readonly IExcelGenerator _excelGenerator;
        public ReportGenerator(IReportRepository reportRepository,
        IConfiguration config,
        IWebServiceRequest webServiceRequest,
        IExcelGenerator excelGenerator)
        {
            _reportRepository = reportRepository;
            _config = config;
            _webServiceRequest = webServiceRequest;
            _excelGenerator = excelGenerator;
        }

        public async Task GenerateReport(Guid reportRequestId)
        {
            var allReportData = new List<ReportDto>();
            var reportData = await _webServiceRequest.GetReportData();
            allReportData.AddRange(reportData);
            
            for (int i = 0; i < reportData.PageCount - 1; i++)
            {
                reportData = await _webServiceRequest.GetReportData();
                allReportData.AddRange(reportData);
            }

            var excelPath = await _excelGenerator.ExportToExcel(allReportData);
            await _reportRepository.UpdateReportStatus(reportRequestId, excelPath);
        }
    }
}