using System;

namespace ServiceHub.Interfaces.Services
{
    public interface IFileService
    {
        bool Exists(string? path);
        DateTime GetLastWriteTime(string path);
    }
}
