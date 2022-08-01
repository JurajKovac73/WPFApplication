using System.Threading;
using ServiceHub.Interfaces.Services;


namespace ServiceHub.Interfaces.ServicesConfig
{
    public interface IFileMonitoringServiceConfig
    {
        public string FilePath { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public IFileService FileService { get; set; }
        public IPeriodicTimer PeriodicTimer { get; set; }
    }
}
