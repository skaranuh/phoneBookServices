using System.Collections.Generic;
using System.Text.Json.Serialization;
using X.PagedList;

namespace PhoneBook.Common.Dtos
{
    public class PageListToSerialize<T>
    {
        [JsonPropertyName("list")]
        public List<T> List { get; set; }

        [JsonPropertyName("metaData")] 
        public PagedListMetaData MetaData { get; set; }
    }
}