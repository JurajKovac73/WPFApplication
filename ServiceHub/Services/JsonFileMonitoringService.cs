using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ServiceHub.Interfaces.Services;
using ServiceHub.Interfaces.ServicesConfig;

namespace ServiceHub.Services
{
    public sealed class JsonFileMonitoringService : IFileMonitoringService
    {
       private readonly IFileService _fileService;
       private readonly IPeriodicTimer _periodicTimer;
       private readonly CancellationTokenSource _cancellationTokenSource;    
       private readonly string _filePath;
       private readonly object _lock = new object();
       
       private DateTime _lastFileModifiedDate;
       private bool _hasFileChanged;
       private bool _isRunning;

       public event EventHandler? FileChanged;

        public JsonFileMonitoringService(IFileMonitoringServiceConfig config)
        {
            _filePath = config.FilePath;
            _periodicTimer = config.PeriodicTimer;
            _cancellationTokenSource = config.CancellationTokenSource;
            _fileService = config.FileService;
        }

        public async Task Start()
        {
            _isRunning = true;
            try
            {
                while (await _periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token))
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        _isRunning = false;
                        break;
                    }

                    CheckFileChanges();
                }
            }
            catch 
            {
                MessageBox.Show("Program was unable to check JSON file.");
                //TODO: only simple implementation for lack of time, some logging and better handling would be better.
            }
            finally
            {
                _periodicTimer.Dispose();
            }
        }

       public async Task Stop()
       {
            if (_isRunning)
            {
                _cancellationTokenSource.Cancel();
                await _periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token);
                _isRunning = false;
            }
           _periodicTimer.Dispose();
       }

       private void CheckFileChanges()
       {
           if (!_fileService.Exists(_filePath))
           {
               return;
           }

           DateTime currentLastModified = _fileService.GetLastWriteTime(_filePath);
           lock (_lock)
           {
               if (currentLastModified != _lastFileModifiedDate)
               {
                    _lastFileModifiedDate = currentLastModified;
                    _hasFileChanged = true;
               }
               else
               {
                    _hasFileChanged = false;
               }
           }
           
           if (_hasFileChanged)
           {
                FileChanged?.Invoke(this, EventArgs.Empty);
           }
       }
    }
}
