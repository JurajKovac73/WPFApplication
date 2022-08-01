using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceHub.Interfaces.Services
{
    public interface IPeriodicTimer : IDisposable
    {
        ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken = default);
    }
}
