using System;
using PhoneBook.Report.Api.Entities.Entities.Base;
using PhoneBook.Report.Api.Entities.Enums;

namespace PhoneBook.Report.Api.Entities.Entities
{
    public class ReportEntity : BaseEntity
    {
        public ReportStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
        public string ReportPath { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}