using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Report.Api.Controllers;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;
using Xunit;

namespace PhoneBook.Report.Api.Tests
{
    public class ReportControllerTests
    {
        [Fact]
        public async Task CreateReportRequest_Should_Create_A_Report_Request()
        {
            //arrange
            var reportService = new Mock<IReportService>();
            var reportController = new ReportController(reportService.Object);

            var reportResponseDto = new ReportResponseDto { Id = Guid.NewGuid(), RequestDate = DateTime.Now, Status = Services.Enums.ReportStatus.Pending };
            reportService.Setup(x => x.CreateReportRequest()).ReturnsAsync(reportResponseDto);

            //act
            var actionResult = await reportController.CreateReportRequest();
            var okObjectResult = actionResult as OkObjectResult;

            Assert.Equal(okObjectResult.Value, reportResponseDto);
        }

        [Fact]
        public async Task ListReportRequests_Should_List_Report_Requests()
        {
            //arrange
            var reportService = new Mock<IReportService>();
            var reportRequests = new List<ReportResponseDto>();
            reportService.Setup(x => x.ListReportRequests()).ReturnsAsync(reportRequests);
            var reportController = new ReportController(reportService.Object);

            //act
            var actionResult = await reportController.ListReportRequests();
            var okObjectResult = actionResult as OkObjectResult;

            //assert
            reportService.Verify(x => x.ListReportRequests());
            Assert.Equal(reportRequests, okObjectResult.Value);
            Assert.Equal(200, okObjectResult.StatusCode);
        }
    }
}