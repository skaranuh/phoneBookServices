using System;
using System.Threading.Tasks;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IMessagePublisher
    {
        Task Publish(string topic, string message);
    }
}