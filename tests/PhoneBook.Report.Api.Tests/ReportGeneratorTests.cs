using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Entities.Entities;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Services.Implementations;
using PhoneBook.Report.Api.Services.Interfaces;
using X.PagedList;
using Xunit;
using System.Linq;

namespace PhoneBook.Report.Api.Tests
{
    public class ReportGeneratorTests
    {
        [Fact]
        public async Task Generate_Report_Should_Generate_Report()
        {
            //arrange
            var reportRepository = new Mock<IReportRepository>();
            var configuration = new Mock<IConfiguration>();
            var webServiceRequest = new Mock<IWebServiceRequest>();
            var excelGenerator = new Mock<IExcelGenerator>();
            var reportGenerator = new ReportGenerator(reportRepository.Object, configuration.Object, webServiceRequest.Object, excelGenerator.Object);
            var reportRequestId = Guid.NewGuid();
            var reportData = new List<ReportDto>();
            var reportItemsCount = 5;
            for (int i = 0; i < reportItemsCount; i++)
            {
                reportData.Add(new ReportDto { Location = $"location{i + 1}", PersonsCount = 1, PhoneNumbersCount = 2 });
            }

            var reportPath = "reportPath";

            var pageNumber = 1;
            var pageSize = 1;
            var reportRequestsPagedList = new PagedList<ReportDto>(reportData.AsQueryable(), pageNumber, pageSize);
            var serviceCallTimes = (int)Math.Ceiling((decimal)reportItemsCount / (decimal)pageSize);

            configuration.Setup(x => x["Report:PageSize"]).Returns(pageSize.ToString());
            webServiceRequest.Setup(x => x.GetReportData(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(reportRequestsPagedList);
            excelGenerator.Setup(x => x.ExportToExcel(It.Is<IEnumerable<ReportDto>>(x => x.ToList().Count == reportData.Count))).ReturnsAsync(reportPath);

            //act
            await reportGenerator.GenerateReport(reportRequestId);

            //assert
            webServiceRequest.Verify(x => x.GetReportData(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(serviceCallTimes));
            excelGenerator.Verify(x => x.ExportToExcel(It.Is<IEnumerable<ReportDto>>(x => x.ToList().Count == reportData.Count)));
            reportRepository.Verify(x => x.UpdateReportStatus(reportRequestId, reportPath));
        }
    }
}