using System.Collections.Generic;
using System.Data;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IDataTableConverter
    {
        DataTable ToDataTable<T>(List<T> items);
    }
}