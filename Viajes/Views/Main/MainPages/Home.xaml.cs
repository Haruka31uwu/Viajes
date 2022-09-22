using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Users _u;
        ServicesRepository sr;
        public Home(Users u)
        {
            _u = u;
            InitializeComponent();
            sr = new ServicesRepository();
            BindingContext = sr;
                new Action(async () =>
            {
                lServices.ItemsSource = await lista();
            }).Invoke();
            
        }
        public async Task<IEnumerable<Services>> lista()
        {
            return await sr.GetallServices();        
        }

        private async void lServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lServices.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                lServices.IsEnabled = true;
                return false;
            });
            var dataitem =(Services) e.Item;
            bool answer = await DisplayAlert("Informacion", "Desea ver mas informacion", "Yes", "No");
            
            if (answer == true)
            {
                await Navigation.PushAsync(new ViewPage(dataitem,_u));
            }
        }
    }
}