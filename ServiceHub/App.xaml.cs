using System;
using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHub.Interfaces.Services;
using ServiceHub.Services;
using ServiceHub.Services.ServicesConfig;
using ServiceHub.ViewModels;
using ServiceHub.Views;


namespace ServiceHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly string _filePath = Path.Combine(Environment.CurrentDirectory, @"ServiceHub.json");
        public App()
        {
            var jsonFileMonitoringServiceConfig = new JsonFileMonitoringServiceConfig
            {
                FilePath = _filePath,
                CancellationTokenSource = new CancellationTokenSource(),
                FileService = new FileService(),
                PeriodicTimer = new PeriodicTimerService(TimeSpan.FromSeconds(2))
            };

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
            {
                services.AddSingleton<SystemReportViewModel>();
                services.AddSingleton<IFileMonitoringService, JsonFileMonitoringService>(
                    provider => new JsonFileMonitoringService(jsonFileMonitoringServiceConfig));
                services.AddSingleton<IGetFileDataService, GetJsonFileDataService>();
                services.AddSingleton<SystemReportView>();
            })
                .Build();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var viewModel = new SystemReportViewModel(_host.Services.GetRequiredService<IGetFileDataService>(), 
                _host.Services.GetRequiredService<IFileMonitoringService>(), 
                _filePath);
            var view = new SystemReportView();
            view.DataContext = viewModel;

            MainWindow.Show();
            
            _host.Services.GetRequiredService<IFileMonitoringService>().Start();
            base.OnStartup(e);

        }     
 
        protected override void OnExit(ExitEventArgs e)
        {
            _host.Services.GetRequiredService<IFileMonitoringService>().Stop();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
