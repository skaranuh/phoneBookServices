using System.IO;
using System.Threading.Tasks;
using PhoneBook.Report.Api.Services.Interfaces;

namespace PhoneBook.Report.Api.Services.Implementations
{
    public class FileOperations : IFileOperations
    {
        public async Task SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            var info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            var path = Path.Combine(filePath, fileName);
            using (var outputFileStream = new FileStream(path, FileMode.Create))
            {
               await inputStream.CopyToAsync(outputFileStream);
            }
        }
    }
}

