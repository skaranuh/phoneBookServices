using System.Collections.Generic;
using System.Text.Json.Serialization;
using PhoneBook.Common.Dtos;
using PhoneBook.Report.Api.Entities.Entities;
using X.PagedList;

namespace PhoneBook.Report.Api.Services.Dtos
{
    public class PageListToDeserialize<T>
    {
        [JsonPropertyName("list")]
        public ListItems<T> List { get; set; }
        [JsonPropertyName("metaData")]
        public PagedListMetaData MetaData { get; set; }
    }

    public class ListItems<T>
    {
        [JsonPropertyName("$values")]
        public List<T> Values { get; set; }
    }
}