using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceHub.Interfaces.Services;

namespace ServiceHub.Services
{
    internal sealed class PeriodicTimerService : IPeriodicTimer
    {
        private readonly PeriodicTimer _periodicTimer;

        public PeriodicTimerService(TimeSpan timeSpan)
            => _periodicTimer = new PeriodicTimer(timeSpan);

        public async ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken = default)
            => await _periodicTimer.WaitForNextTickAsync(cancellationToken);

        public void Dispose() => _periodicTimer.Dispose();
    }
}
