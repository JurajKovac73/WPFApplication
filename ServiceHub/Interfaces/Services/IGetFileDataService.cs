using System.Threading.Tasks;
using ServiceHub.Models;

namespace ServiceHub.Interfaces.Services
{
    public interface IGetFileDataService
    {
        public Task<SystemReportModel> GetFileData(string filePath);

        public SystemReportModel GetInitialFileData(string filePath);
    }
}
