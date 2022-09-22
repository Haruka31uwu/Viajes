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
        public BuyOrderRepository bo;
        public ServicesRepository sr;
        public BuyOrder _bo;
        public Users _u;
        
        List<Model.Services>? ls;
        Model.BuyCar car;
        public BuyCar(Users u)
        {
            InitializeComponent();
            _u = u;
            cv = new CarViewModel();
            sr = new ServicesRepository();
            bo = new BuyOrderRepository();
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
    }

    private async void lServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lCar.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                lCar.IsEnabled = true;
                return false;
            });
            bool ok = await DisplayAlert("Que Desea hacer?","Desea quitar del carrito o proceder con el pago?", "Comprar", "Borrar");
            int? index = null;
            if (!ok)
            {
                car = await cv.GetCarForId(_u.Id);
                if (car != null)
                {
                  
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
                           
                        }
                        i++;

                    }
                    if (index != null)
                    {
                        await cv.DeleteFromCar(index, _u.Id);
                        
                        await DisplayAlert("Exito", "Borrado con exito", "Ok");
                        lCar.ItemsSource = await lista();
                    }
                }
            }
            else
            {

                List<BuyOrder> __bo;
                __bo= new List<BuyOrder>();
                __bo = await bo.GetallOrder();
                var  data = (Services)e.Item;
                _bo = new BuyOrder
                {
                    UserId = _u.Id,
                    IdOrder = (__bo.Count + 1).ToString(),
                    ServiceOrderElement = data,
                    PriceOrder = data.Price.ToString()
                    
                };
              
                car = await cv.GetCarForId(_u.Id);
                
                if (car != null)
                {

                    for (int i = 0; i < car?.ServiceList.Count; i++)
                    {
                        var dat = (Services)e.Item;
                        if (car?.ServiceList[i].IdOfService == dat.IdOfService)
                        {
                            //Debug.WriteLine("Exito");
                            index = i;
                        }
                        else
                        {

                        }
                        {

                        }
                        i++;

                    }
                    if (index != null)
                    {
                        await bo.Save(_bo);
                        await cv.DeleteFromCar(index, _u.Id);
                        await cv.DeleteCar();
                        await sr.UpdateRow(data);
                        await DisplayAlert("Exito", "Compra  Realizada con exito", "Ok");
                        lCar.ItemsSource = await lista();
                    }
                }
               
            }
        }
       
        
    

        
    }
}