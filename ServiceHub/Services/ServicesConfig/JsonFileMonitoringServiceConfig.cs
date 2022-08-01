using System;
using System.IO;
using System.Threading;
using ServiceHub.Interfaces.Services;
using ServiceHub.Interfaces.ServicesConfig;

namespace ServiceHub.Services.ServicesConfig
{
    public sealed class JsonFileMonitoringServiceConfig : IFileMonitoringServiceConfig
    {
        public string FilePath { get; set; } = Path.Combine(Environment.CurrentDirectory, @"ServiceHub.json");
        public CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();

        public IFileService FileService { get; set; } = new FileService();

        public IPeriodicTimer PeriodicTimer { get; set; } = new PeriodicTimerService(TimeSpan.FromSeconds(2));
    }
}
