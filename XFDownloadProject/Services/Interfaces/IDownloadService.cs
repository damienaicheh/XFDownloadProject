using System;
using System.Threading;
using System.Threading.Tasks;

namespace XFDownloadProject.Services.Interfaces
{
    public interface IDownloadService
    {
        Task DownloadFileAsync(string url, IProgress<double> progress, CancellationToken token);
    }
}
