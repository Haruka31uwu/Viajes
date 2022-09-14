using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class ViewPage : ContentPage
    {
        public string id,Price,name;
        CarViewModel car;
        public ViewPage(string Ns,string Des,string Data,string ig,string idofs,string price)
        {
            InitializeComponent();
            car = new CarViewModel();
            name = Ns;
            img.Source = ig;
            sn.Text = name;
            des.Text = Des;
            dt.Text = Data;
            id = idofs;
            Price = price;
            
        }

        private  async void Button_Clicked(object sender, EventArgs e)
        {
           if(String.IsNullOrEmpty(id)|| String.IsNullOrEmpty(Price)|| String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "No se pudo Agregar al carrito", "Ok");
            }
            else
            {
                try
                {
                    Model.BuyCar bc = new Model.BuyCar();
                   
                    //bc.PriceCar = float.Parse(Price);
                    var isSaved = await car.Save(bc);
                    if (isSaved)
                    {
                        Debug.WriteLine("Funciono uwu");
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            
        }
    }
}