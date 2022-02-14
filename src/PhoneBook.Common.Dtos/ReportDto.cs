using System.Text.Json.Serialization;

namespace PhoneBook.Common.Dtos
{

    public class ReportDto
    {
        [JsonPropertyName("personsCount")]
        public int PersonsCount { get; set; }
        [JsonPropertyName("phoneNumbersCount")]
        public int PhoneNumbersCount { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}