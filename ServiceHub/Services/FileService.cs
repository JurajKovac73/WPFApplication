using System;
using System.IO;
using ServiceHub.Interfaces.Services;

namespace ServiceHub.Services
{
    internal sealed class FileService : IFileService
    {
        public bool Exists(string? path)
        {
           return File.Exists(path);
        }

        public DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }
    }
}
