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
    public partial class BuyCar : ContentPage
    {
        public CarViewModel cv;
        public Users _u;
        List<Model.Services>? ls;
        Model.BuyCar car;
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
                ls = bc?.ServiceList;
                

                 return ls;
             }catch(Exception ex)
             {
                 await DisplayAlert("Error", ex.Message, "OK");
                 return null;
             }


<<<<<<< HEAD
            l = new List<Model.Services>();
            //l= cv.GetCarForId(_u.Id).Result.ServiceList;
            return l;
=======
>>>>>>> 3bc7327c06dbf273aaf9b17efa4b8d15e387e047
            

    }

    private async void lServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool ok = await DisplayAlert("Que Desea hacer?","Desea quitar del carrito o proceder con el pago?", "Comprar", "Borrar");
            if (!ok)
            {
                car = await cv.GetCarForId(_u.Id);
                if (car != null)
                {
                    int? index = null;
                    for (int i = 0; i < car?.ServiceList.Count; i++)
                    {
                        var data = (Services)e.Item;
                        if (car?.ServiceList[i].IdOfService == data.IdOfService)
                        {
                            //Debug.WriteLine("Exito");
                            index = i;
                        }
                        else
                        {
                            //Debug.WriteLine("Fallo");
                        }
                        i++;

                    }
                    if (index != null)
                    {
                        await cv.DeleteFromCar(index, _u.Id);
                        await DisplayAlert("Exito", "Borrado con exito", "Ok");
                    }
                }

            }
            else
            {

            }

        }

        
    }
}