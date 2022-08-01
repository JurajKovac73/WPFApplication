using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MvvmHelpers.Commands;
using ServiceHub.Interfaces.Services;
using ServiceHub.Models;

namespace ServiceHub.ViewModels
{
    internal partial class SystemReportViewModel : ViewModelBase
    {
        private readonly IGetFileDataService _getFileDataService;
        private readonly IFileMonitoringService _fileMonitoringService;
        private readonly string _filePath;

        public ICommand GetJsonFileDataCommand { get; }

        private string? _systemId;
        public string? SystemId
        {
            get => _systemId;
            set => SetProperty(ref _systemId, value);
        }

        private string? _systemName;
        public string? SystemName
        {
            get => _systemName;
            set => SetProperty(ref _systemName, value);
        }

        private string? _systemDescription;
        public string? SystemDescription
        {
            get => _systemDescription;
            set => SetProperty(ref _systemDescription, value);
        }

        private string? _status;
        public string? Status 
        { 
          get => _status;
          set => SetProperty(ref _status, value); 
        }

        private bool _isRunning;
        public bool IsRunning 
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value); 
        }



        public SystemReportViewModel(IGetFileDataService getFileDataService,
            IFileMonitoringService fileMonitoringService,
            string filePath)
        {
            _getFileDataService = getFileDataService;
            _fileMonitoringService = fileMonitoringService;
            _filePath = filePath;
            GetJsonFileDataCommand = new AsyncCommand(GetFileData);
            _fileMonitoringService.FileChanged += OnFileChanged;

            SystemReportModel systemReportModel = _getFileDataService.GetInitialFileData(_filePath);
            UpdateViewModel(systemReportModel);
        }

        public async Task GetFileData()
        { 
            try
            {
                SystemReportModel systemReportModel = await _getFileDataService.GetFileData(_filePath);
                UpdateViewModel(systemReportModel);
            }
            catch 
            {
                MessageBox.Show("Unable to read from JSON file, please make sure it has correct structure.");
                //TODO: only simple implementation for lack of time, some logging and better handling would be better.
            }
        }

        private void UpdateViewModel(SystemReportModel systemReportModel)
        {
            SystemId = systemReportModel.SystemID;
            SystemName = systemReportModel.SystemName;
            SystemDescription = systemReportModel.SystemDescription;
            Status = systemReportModel.Status;
            IsRunning = systemReportModel.IsRunning;
        }
        private async void OnFileChanged(object? sender, EventArgs? e)
        {
            await GetFileData();
        }
    }
}
