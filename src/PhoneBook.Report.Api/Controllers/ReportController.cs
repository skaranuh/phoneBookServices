using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> CreateReportRequest()
        {
            var response = await _reportService.CreateReportRequest();
            return Ok(response);
        }

        public async Task<IActionResult> ListReportRequests(int pageNumber, int pageSize)
        {
             var response = await _reportService.ListReportRequests(pageNumber, pageSize);
            return Ok(response);
        }
    }
}
