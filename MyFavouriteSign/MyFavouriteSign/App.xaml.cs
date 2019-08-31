using Microsoft.AppCenter;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Data;
using MyFavouriteSign.Services;
using Xamarin.Forms;

namespace MyFavouriteSign
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=<insertappcentersecrethere>;", typeof(Auth), typeof(Data));
            await Authentication.SignInAsync();
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
