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
        public async Task<IEnumerable<Services?>> lista()
        {

            l = new List<Model.Services>();
            //l= cv.GetCarForId(_u.Id).Result.ServiceList;
            return l;
            
           
        }

        private void lServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}