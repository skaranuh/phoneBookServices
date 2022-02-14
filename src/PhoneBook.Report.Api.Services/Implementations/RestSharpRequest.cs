using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Services.Dtos;
using PhoneBook.Report.Api.Services.Interfaces;
using RestSharp;
using X.PagedList;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class RestSharpRequest : IRestSharpRequest
    {
        private readonly IConfiguration _configuration;
        public RestSharpRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<PageListToDeserialize<ReportDto>> GetReportData(int pageNumber, int pageSize)
        {
            var baseUrl = _configuration["Report:BaseUrl"];
            var client = new RestClient($"{baseUrl}");
            var relativeUrl = $"{ _configuration["Report:Resource"]}?pageNumber={pageNumber}&pageSize={pageSize}";
            var request = new RestRequest(relativeUrl, RestSharp.Method.Get);
            var response = await client.ExecuteAsync(request);
            var result = JsonSerializer.Deserialize<PageListToDeserialize<ReportDto>>(response.Content);
            return result;
        }
    }
}