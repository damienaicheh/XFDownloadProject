using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using XFDownloadProject.Services.Interfaces;

namespace XFDownloadProject.ViewModels
{
    public class DownloadViewModel : ViewModelBase
    {
        private double _progressValue;
        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>The progress value.</value>
        public double ProgressValue
        {
            get { return _progressValue; }
            set { Set(ref _progressValue, value); }
        }

        private bool _isDownloading;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:XFDownloadProject.ViewModels.DownloadViewModel"/>
        /// is downloading.
        /// </summary>
        /// <value><c>true</c> if is downloading; otherwise, <c>false</c>.</value>
        public bool IsDownloading
        {
            get { return _isDownloading; }
            set { Set(ref _isDownloading, value); }
        }

        /// <summary>
        /// The download service.
        /// </summary>
        private readonly IDownloadService _downloadService;

        /// <summary>
        /// Gets the start download command.
        /// </summary>
        /// <value>The start download command.</value>
        public ICommand StartDownloadCommand { get; }

        public DownloadViewModel(IDownloadService downloadService)
        {
            _downloadService = downloadService;
            StartDownloadCommand = new RelayCommand(async () => await StartDownloadAsync());
        }

        /// <summary>
        /// Starts the download async.
        /// </summary>
        /// <returns>The download async.</returns>
        public async Task StartDownloadAsync()
        { 
            var progressIndicator = new Progress<double>(ReportProgress);
            var cts = new CancellationTokenSource();
            try
            {
                IsDownloading = true;

                var url = "https://github.com/damienaicheh/XamarinAndroidParcelable/archive/master.zip";

                await _downloadService.DownloadFileAsync(url, progressIndicator, cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //Manage cancellation here
            }
            finally
            {
                IsDownloading = false;
            }
        }

        /// <summary>
        /// Reports the progress status for the downlaod.
        /// </summary>
        /// <param name="value">Value.</param>
        internal void ReportProgress(double value)
        {
            ProgressValue = value;
            System.Diagnostics.Debug.WriteLine(value.ToString());
        }       
    }
}
