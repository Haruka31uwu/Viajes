using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ViewPage : ContentPage
    {

        public string id, Price, name;
        CarViewModel car;
        List<Services> lserv;
        public Services serv;
        public Model.BuyCar buycar;
        public int tp;
        public Users _u;
        public ViewPage(Services s,Users u)
        {
            
            InitializeComponent();
            _u = u;
            serv = s;
            //Debug.WriteLine(serv);
            car = new CarViewModel();       
            //lserv = buycar.ServiceList;
            //lserv.Add(s);
            /*new Action(async () => lserv = await car.GetCarForId("1")).Invoke();
            lserv.Add(new BuyCar() { 
            idOfUser=1.ToString(),
            PriceCar=0,
            ServiceList
            );*/
            /*buycar = new BuyCar()
            {
                idOfUser = 1.ToString(),
                ServiceList =lserv,
                PriceCar = s.Price,
            };*/
            //
           /* foreach (Services l in lserv)
            {
                tp += l.Price;
            }
           */
            name = s.NameOfService;
            img.Source = s.ImageName;
            sn.Text = name;
            des.Text = s.Destination;
            dt.Text = s.Data;
            id = s.IdOfService;
            Price = s.Price.ToString();
           

        }public async Task regCar()
        {
            
            buycar = await car.GetCarForId(_u.Id);
            Debug.WriteLine(buycar?.ServiceList);
            if (buycar == null)
            {

                Debug.WriteLine("owo");
                lserv = new List<Services>();
                lserv.Add(serv);
                Model.BuyCar buycar1 = new Model.BuyCar()
                {
                    idOfUser = _u.Id,
                    ServiceList = lserv,
                    PriceCar = serv.Price,
                };

                await car.Save(buycar1);
               ;
            }
            else if (buycar?.ServiceList == null && buycar != null)
            {
              
                //buycar = await car.GetCarForId(_u.Id);
                await car.DeleteCar();
                //Debug.WriteLine("uwu");
                lserv = new List<Services>();
                lserv.Add(serv);
                Debug.WriteLine("uwu");
                Model.BuyCar buycar1 = new Model.BuyCar()
                {
                    idOfUser = _u.Id,
                    ServiceList = lserv,
                    PriceCar = serv.Price,
                };

                await car.Save(buycar1);
                ;
            }
            else
            {
                lserv = buycar?.ServiceList;
                lserv?.Add(serv);
                await car.UpdateRow(lserv, _u.Id, precio(lserv));
            }
        }public int precio(List<Services> li)
        {
            int precio=0;
            foreach(Services l in li)
            {
                precio=precio +l.Price;
            }
            return precio;
        }

        private async void Button_Clicked(object sender, EventArgs e)
            
        {
            bt1.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {

                bt1.IsEnabled = true;
                return false;
            });
            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(Price) || String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "No se pudo Agregar al carrito", "Ok");
            }
            else
            {
                try
                {
                   
                    await regCar();
                    await DisplayAlert("Exito", "Se Añadio al carrito con Exito", "Ok");
                    await Navigation.PushModalAsync(new Views.Main.MainPage(_u));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


            }
        }
    } 
}