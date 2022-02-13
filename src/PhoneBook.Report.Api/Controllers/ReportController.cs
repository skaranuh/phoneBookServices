using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Controllers
{
    [ApiController]
    [Route("api/phoneBookReport")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("reportRequests")]
        public async Task<IActionResult> CreateReportRequest()
        {
            var response = await _reportService.CreateReportRequest();
            return Ok(response);
        }

        [HttpGet]
        [Route("reportRequests")]
        public async Task<IActionResult> ListReportRequests(int pageNumber, int pageSize)
        {
            var response = await _reportService.ListReportRequests(pageNumber, pageSize);
            return Ok(response);
        }
    }
}
