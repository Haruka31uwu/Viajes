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
        public BuyCar buycar;
        public int tp;
        public ViewPage(Services s)
        {
            InitializeComponent();
            serv = s;
            Debug.WriteLine(serv);
            car = new CarViewModel();
            Debug.WriteLine("1");
            
            Debug.WriteLine("2");
            new Action(async () => await regCar() ).Invoke();
            Debug.WriteLine("3");
           
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
           
            buycar = await car.GetCarForId("1");
            if (buycar!=null)
            {
                lserv = buycar.ServiceList;
                lserv.Add(serv);
               

                await car.UpdateRow(lserv, "1",precio(lserv));
              
            }
            else
            {
                lserv.Add(serv);
                buycar = new BuyCar()
                {
                    idOfUser = 1.ToString(),
                    ServiceList = lserv,
                    PriceCar = serv.Price,
                };
                await car.Save(buycar);
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
            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(Price) || String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Error", "No se pudo Agregar al carrito", "Ok");
            }
            else
            {
                try
                {
                    Debug.WriteLine(tp);
                    await car.UpdateRow(lserv, "1", tp);

                    //bc.PriceCar = float.Parse(Price);
                    /*var isSaved = await car.Save(buycar);
                    if (isSaved)
                    {
                        Debug.WriteLine("Funciono uwu");
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }*/
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


            }
        }
    } 
}