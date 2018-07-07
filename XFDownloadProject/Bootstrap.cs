using System;
using GalaSoft.MvvmLight.Ioc;
using XFDownloadProject.Services.Implementations;
using XFDownloadProject.Services.Interfaces;
using XFDownloadProject.ViewModels;

namespace XFDownloadProject
{
    public class Bootstrap
    {
        private static Bootstrap instance;
        /// <summary>
        /// This is a singleton instance for bootstraping the application.
        /// </summary>
        /// <value>The instance.</value>
        public static Bootstrap Instance
        {
            get
            {
                if (instance == null)
                    instance = new Bootstrap();

                return instance;
            }
        }

        /// <summary>
        /// Setup all injections
        /// </summary>
        public void Setup()
        {
            SimpleIoc.Default.Register<IDownloadService, DownloadService>();
            SimpleIoc.Default.Register<DownloadViewModel>();
        }
    }
}
