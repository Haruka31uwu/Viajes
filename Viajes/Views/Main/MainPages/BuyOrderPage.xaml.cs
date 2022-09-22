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
    public partial class BuyOrderPage : ContentPage
    {
        BuyOrderRepository br;
      
        Users _u;
        List<BuyOrder>? bc;
       List<Services?> ls;
        public BuyOrderPage(Users u)
        {
            InitializeComponent();
            
            br = new BuyOrderRepository();
            
            BindingContext = br;
            _u = u;
            new Action(async () => lBuy.ItemsSource = await lista()).Invoke();
            

        }
        public async Task<IEnumerable<Services>?> lista()
        {
            try
            {
                bc = await br.GetOrderForId(_u.Id);
                ls = new List<Services?>();
                for (int i = 0; i < bc?.Count; i++) {
                    ls.Add(bc[i].ServiceOrderElement);
                }
                return ls;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return null;
            }

        }
    }
}