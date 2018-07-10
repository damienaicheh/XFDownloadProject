using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.Services.Implementations
{
    public class DownloadService : IDownloadService
    {
        /// <summary>
        /// The http client.
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// The file service.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// The size of the buffer.
        /// </summary>
        private int bufferSize = 4095;


        public DownloadService(IFileService fileService)
        {
            _client = new HttpClient();
            _fileService = fileService;
        }

        /// <summary>
        /// Downloads the file async.
        /// </summary>
        /// <returns>The file async.</returns>
        /// <param name="url">URL.</param>
        /// <param name="progress">Progress.</param>
        /// <param name="token">Token.</param>
        public async Task DownloadFileAsync(string url, IProgress<double> progress, CancellationToken token)
        {
            try
            {
                // Step 1 : Get call
                var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
                }

                // Step 2 : Filename
                var fileName = response.Content.Headers?.ContentDisposition?.FileName ?? "tmp.zip";

                // Step 3 : Get total of data
                var totalData = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
                var canSendProgress = totalData != -1 && progress != null;

                // Step 4 : Get total of data
                var filePath = Path.Combine(_fileService.GetStorageFolderPath(), fileName);

                // Step 5 : Download data
                using (var fileStream = OpenStream(filePath))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var totalRead = 0L;
                        var buffer = new byte[bufferSize];
                        var isMoreDataToRead = true;

                        do
                        {
                            token.ThrowIfCancellationRequested();

                            var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                            if (read == 0)
                            {
                                isMoreDataToRead = false;
                            }
                            else
                            {
                                // Write data on disk.
                                await fileStream.WriteAsync(buffer, 0, read);

                                totalRead += read;

                                if (canSendProgress)
                                {
                                    progress.Report((totalRead * 1d) / (totalData * 1d) * 100);
                                }
                            }
                        } while (isMoreDataToRead);
                    }
                }
            }
            catch (Exception e)
            {
                // Manage the exception as you need here.
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Opens the stream.
        /// </summary>
        /// <returns>The stream.</returns>
        /// <param name="path">Path.</param>
        private Stream OpenStream(string path)
        {
            return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize);
        }
    }
}
