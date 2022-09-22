using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Viajes.Views;
using Viajes.Views.Admin_Views;
using Viajes.Views.Main.MainPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes
{
    public partial class App : Application
    {
        public UsersRepository ur;

        public App()
        {
            InitializeComponent();
            ur = new UsersRepository();


            MainPage = new NavigationPage(new MainPage());
            //MainPage = new MainPage();
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