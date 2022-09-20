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
    public partial class BuyCar : ContentPage
    {
        public CarViewModel cv;
        public Users _u;
        List<Model.Services>? l;
        public BuyCar(Users u)
        {
            InitializeComponent();
            _u = u;
            cv = new CarViewModel();
            new Action(async () =>
            {
                lCar.ItemsSource = await lista();
            }).Invoke();
        }
        public async Task<IEnumerable<Services>?> lista()
         {
             try
             {
               Model.BuyCar? bc=await cv.GetCarForId(_u.Id);
                l = bc?.ServiceList;
                

                 return l;
             }catch(Exception ex)
             {
                 await DisplayAlert("Error", ex.Message, "OK");
                 return null;
             }


            

    }

    private void lServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}