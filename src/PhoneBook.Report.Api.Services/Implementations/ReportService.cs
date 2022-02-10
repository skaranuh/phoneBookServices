using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Repositories.Interfaces;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;

        public ReportService(IReportRepository reportRepository, IMapper mapper, IMessagePublisher messagePublisher)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }
        public async Task<ReportResponseDto> CreateReportRequest()
        {
            var report = await _reportRepository.CreateReportRequest();
            var reportResponse = _mapper.Map<ReportResponseDto>(report);
            await _messagePublisher.Publish(reportResponse.Id);
            return reportResponse;
        }

        public Task<PageListToSerialize<ReportResponseDto>> ListReportRequests()
        {
            throw new System.NotImplementedException();
        }
    }
}