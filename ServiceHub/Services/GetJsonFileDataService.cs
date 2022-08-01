using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ServiceHub.Interfaces.Services;
using ServiceHub.Models;

namespace ServiceHub.Services
{
    public sealed class GetJsonFileDataService : IGetFileDataService
    {
        public async Task<SystemReportModel> GetFileData(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader streamReader = new(filePath))
                    {
                        var jsonData = await streamReader.ReadToEndAsync();
                        if (!string.IsNullOrWhiteSpace(jsonData))
                        {
                            return JsonConvert.DeserializeObject<SystemReportModel>(jsonData) ?? new SystemReportModel();
                        }
                    }
                }
                catch {
                    MessageBox.Show("Unable to read from JSON file, please make sure it has correct structure.");
                    //TODO: only simple implementation for lack of time, some logging and better handling would be better.
                }
            }

            return new SystemReportModel();
        }

        public SystemReportModel GetInitialFileData(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader streamReader = new(filePath))
                    {
                        var jsonData = streamReader.ReadToEnd();
                        if (!string.IsNullOrWhiteSpace(jsonData))
                        {
                            return JsonConvert.DeserializeObject<SystemReportModel>(jsonData) ?? new SystemReportModel();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Unable to read from JSON file, please make sure it has correct structure.");
                    //TODO: only simple implementation for lack of time, some logging and better handling would be better.
                }
            }

            return new SystemReportModel();
        }
    }
}
