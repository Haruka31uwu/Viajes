using System;
using Viajes.Views;
using Viajes.Views.Admin_Views;
using Viajes.Views.Main.MainPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.Main.MainPage();
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
