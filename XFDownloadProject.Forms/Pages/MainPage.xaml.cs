using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XFDownloadProject.ViewModels;

namespace XFDownloadProject.Forms.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = SimpleIoc.Default.GetInstance<DownloadViewModel>();
        }
    }
}
