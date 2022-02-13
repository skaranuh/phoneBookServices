using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Report.Api.Services.Interfaces
{
    public interface IMessageReceiver:IDisposable
    {
        string Receive(CancellationToken cancellationToken);

        void Subscribe(string topic);
    }
}