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
    public partial class Search : ContentPage
    {
         ServicesRepository sr;
        Users _u;
        public Search(Users u)
        {
            InitializeComponent();
            _u = u;
            sr = new ServicesRepository();
            BindingContext = sr;
            new Action(async () => lservices.ItemsSource = await GetSfpriori()).Invoke();
            new Action(async () => lcategory.ItemsSource = await GetSfpriori()).Invoke();

        }
        public async Task<IEnumerable<Services>> GetSfpriori()
        {
            return await sr.GetallServicesforPriority();
        }

        private async void lservices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lservices.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                lservices.IsEnabled = true;
                return false;
            });
            var dataitem = (Services)e.Item;
            await Navigation.PushAsync(new ViewPage(dataitem,_u));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            search.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                search.IsEnabled = true;
                return false;
            });
            if (!String.IsNullOrWhiteSpace(es.Text))
            {
                var dataitem = sr.GetServiceForName(es.Text.ToString());
                es.Text = "";
                Navigation.PushAsync(new SearchPage(dataitem,_u));
                
            }
        }

        private void lcategory_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lcategory.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                lcategory.IsEnabled = true;
                return false;
            });
            var cat=(Services)e.Item;
            var dataitem = sr.GetServiceForCat(cat.DestinationCategory);
            Navigation.PushAsync(new SearchPage(dataitem,_u));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            car.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                car.IsEnabled = true;
                return false;
            });
            Navigation.PushAsync(new BuyCar(_u));
        }
    }
}