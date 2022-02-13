using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _map;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IConfiguration _config;

        public ReportService(IReportRepository reportRepository, IMapper map, IMessagePublisher messagePublisher, IConfiguration config)
        {
            _reportRepository = reportRepository;
            _map = map;
            _messagePublisher = messagePublisher;
            _config = config;
        }
        public async Task<ReportResponseDto> CreateReportRequest()
        {
            var report = await _reportRepository.CreateReportRequest();
            var reportResponse = _map.Map<ReportResponseDto>(report);
            var topic = _config["Messaging:Topic"];
            await _messagePublisher.Publish(topic, reportResponse.Id.ToString());
            return reportResponse;
        }

        public async Task<PageListToSerialize<ReportResponseDto>> ListReportRequests(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            { pageNumber = 1; }

            if (pageSize == 0)
            { pageSize = 100; }

            var reports = await _reportRepository.ListReportRequests(pageNumber, pageSize);
            var ReportResponseDtos = _map.Map<IEnumerable<ReportResponseDto>>(reports);
            var pageListToSerialize = new PageListToSerialize<ReportResponseDto>
            {
                List = ReportResponseDtos,
                MetaData = reports.GetMetaData()
            };

            return pageListToSerialize;
        }
    }
}