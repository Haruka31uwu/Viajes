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
        public Search()
        {
            InitializeComponent();
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
            var dataitem = (Services)e.Item;
            await Navigation.PushAsync(new ViewPage(dataitem));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(es.Text))
            {
                var dataitem = sr.GetServiceForName(es.Text.ToString());
                es.Text = "";
                Navigation.PushAsync(new SearchPage(dataitem));
                
            }
        }

        private void lcategory_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cat=(Services)e.Item;
            var dataitem = sr.GetServiceForCat(cat.DestinationCategory);
            Navigation.PushAsync(new SearchPage(dataitem));
        }
    }
}