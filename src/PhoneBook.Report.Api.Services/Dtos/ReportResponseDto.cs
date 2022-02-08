using System;
using PhoneBook.Report.Api.Services.Enums;

namespace PhoneBook.Report.Api.Services.Dtos
{
    public class ReportResponseDto
    {
        public Guid Id { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}