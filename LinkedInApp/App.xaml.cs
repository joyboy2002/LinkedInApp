using LinkedInApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkedInApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new JobView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
