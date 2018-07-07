using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFDownloadProject.Forms.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFDownloadProject.Forms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // MainPage must bu not null for UWP.
            MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Bootstrap.Instance.Setup();

            MainPage = new MainPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}