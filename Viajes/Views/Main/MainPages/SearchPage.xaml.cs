using System;
using System.Collections.Generic;
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
    public partial class SearchPage : ContentPage
    {
        ServicesRepository _sr;
        Users _u;
        public SearchPage(Task<List<Services>> sr,Users u)
        {
            InitializeComponent();
            _sr=new ServicesRepository();
            BindingContext = _sr;
            _u = u;
            new Action(async () => ls.ItemsSource = await sr).Invoke();
        }

        private async void ls_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ls.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                ls.IsEnabled = true;
                return false;
            });
            var dataitem = (Services)e.Item;
            bool answer = await DisplayAlert("Informacion", "Desea ver mas informacion", "Yes", "No");

            if (answer == true)
            {
                await Navigation.PushAsync(new ViewPage(dataitem, _u));
            }
        }
    }
}