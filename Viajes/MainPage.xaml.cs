using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Xamarin.Forms;

namespace Viajes
{
    public partial class MainPage : ContentPage
    {
        public UsersRepository ur;
        public MainPage()
        {
            InitializeComponent();
            ur = new UsersRepository();
<<<<<<< HEAD
            new Action(async () => start()).Invoke();

        }
        public async void start()
=======
            new Action(async () => await start()).Invoke();

        }
        public async Task start()
>>>>>>> d83ce127103d288d2231e75650c1880d28100daf
        {

            var u = await ur.UserLogedData("harukakasugano31@gmail.com");
            await Navigation.PushModalAsync(new Views.Main.MainPage(u));
        }


    }
}