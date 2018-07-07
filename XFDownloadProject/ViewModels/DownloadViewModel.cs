﻿using System;
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
        public double ProgressValue
        {
            get { return _progressValue; }
            set { Set(ref _progressValue, value); }
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get { return _isDownloading; }
            set { Set(ref _isDownloading, value); }
        }

        private readonly IDownloadService _downloadService;

        public ICommand StartDownloadCommand { get; }

        public DownloadViewModel(IDownloadService downloadService)
        {
            _downloadService = downloadService;
            StartDownloadCommand = new RelayCommand(async () => await StartDownloadAsync());
        }

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

        internal void ReportProgress(double value)
        {
            ProgressValue = value;
            System.Diagnostics.Debug.WriteLine(value.ToString());
        }       
    }
}