using System.IO;
using System.Threading.Tasks;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IFileOperations
    {
        Task SaveStreamAsFile(string filePath, Stream inputStream, string fileName);
    }
}