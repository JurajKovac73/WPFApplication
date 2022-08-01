using System;
using System.Threading.Tasks;

namespace ServiceHub.Interfaces.Services
{
    public interface IFileMonitoringService
    {
       public event EventHandler FileChanged;
       public Task Start();
       public Task Stop();
    }
}
