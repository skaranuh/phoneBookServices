using System.Collections.Generic;
using X.PagedList;

namespace PhoneBook.Common.Dtos
{
    public class PageListToSerialize<T>
    {
        public IEnumerable<T> List { get; set; }
        public PagedListMetaData MetaData { get; set; }
    }
}