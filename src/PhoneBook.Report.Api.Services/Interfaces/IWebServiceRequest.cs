using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Services.Dtos;
using X.PagedList;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IWebServiceRequest
    {
        Task<PageListToDeserialize<ReportDto>> GetReportData(int pageNumber, int pageSize);
    }
}