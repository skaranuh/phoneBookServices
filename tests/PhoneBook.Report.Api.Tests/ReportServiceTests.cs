using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using PhoneBook.Report.Api.Entities.Entities;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Implementations;
using PhoneBook.Report.Api.Services.Interfaces;
using X.PagedList;
using Xunit;

namespace PhoneBook.Report.Api.Tests
{
    public class ReportServiceTests
    {
        [Fact]
        public async Task CreateReportRequest_Should_Save_Report_Request()
        {
            //arrange
            var reportRepository = new Mock<IReportRepository>();
            var reportEntity = new ReportEntity { };

            reportRepository.Setup(x => x.CreateReportRequest()).ReturnsAsync(reportEntity);

            var mapper = new Mock<IMapper>();
            var reportResponse = new ReportResponseDto { };
            mapper.Setup(x => x.Map<ReportResponseDto>(reportEntity)).Returns(reportResponse);

            var messagePublisher = new Mock<IMessagePublisher>();
            var configuration = new Mock<IConfiguration>();

            var reportService = new ReportService(reportRepository.Object, mapper.Object, messagePublisher.Object, configuration.Object);

            //act
            var response = await reportService.CreateReportRequest();

            //assert
            reportRepository.Verify(x => x.CreateReportRequest());
            mapper.Verify(x => x.Map<ReportResponseDto>(reportEntity));
            Assert.Equal(Entities.Enums.ReportStatus.Pending, response.Status);
        }

        [Fact]
        public async Task CreateReportRequest_Should_Queue_Report_Request()
        {
            //arrange
            var reportRepository = new Mock<IReportRepository>();

            var mapper = new Mock<IMapper>();
            var reportResponse = new ReportResponseDto { Id = Guid.NewGuid() };
            mapper.Setup(x => x.Map<ReportResponseDto>(It.IsAny<ReportEntity>())).Returns(reportResponse);
            var configuration = new Mock<IConfiguration>();
            var topic="topic";
            configuration.Setup(x=> x["Kafka:Topic"]).Returns(topic);
            var messagePublisher = new Mock<IMessagePublisher>();
            var reportService = new ReportService(reportRepository.Object, mapper.Object, messagePublisher.Object, configuration.Object);

            //act
            var response = await reportService.CreateReportRequest();

            //assert
            messagePublisher.Verify(x => x.Publish(topic, It.Is<string>(x => x.Equals(reportResponse.Id.ToString()))));
        }

        [Fact]
        public async Task ListReportRequests_Should_List_ReportRequests()
        {
            //arrange
            var reportRepository = new Mock<IReportRepository>();
            var mapper = new Mock<IMapper>();
            var messagePublisher = new Mock<IMessagePublisher>();
            var configuration = new Mock<IConfiguration>();
            var reportService = new ReportService(reportRepository.Object, mapper.Object, messagePublisher.Object, configuration.Object);
            var reportRequests = new List<ReportEntity> { new ReportEntity { Id = Guid.NewGuid(), RequestDate = DateTime.Now, Status = Entities.Enums.ReportStatus.Pending } };

            var pageNumber = 1;
            var pageSize = 1;

            var reportRequestsPagedList = new PagedList<ReportEntity>(reportRequests.AsQueryable(), pageNumber, pageSize);

            var reportRequestDtos = new List<ReportResponseDto> { new ReportResponseDto { Id = Guid.NewGuid(), RequestDate = DateTime.Now, Status = Entities.Enums.ReportStatus.Pending } };
            reportRepository.Setup(x => x.ListReportRequests(pageNumber, pageSize)).ReturnsAsync(reportRequestsPagedList);
            mapper.Setup(x => x.Map<IEnumerable<ReportResponseDto>>(reportRequests)).Returns(reportRequestDtos);

            //act
            var reports = await reportService.ListReportRequests(pageNumber, pageSize);

            //assert
            reportRepository.Verify(x => x.ListReportRequests(pageNumber, pageSize));
            Assert.Equal(reportRequestsPagedList.Count, reports.List.Count());
        }
    }
}