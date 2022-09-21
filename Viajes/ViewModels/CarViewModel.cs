using Firebase.Database;
using Firebase.Database.Query;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;

namespace Viajes.ViewModels
{
 
    public class CarViewModel:BaseViewModel

    {
        public string idOfUser { get; set; }
        public List<Services> ServiceList { get; set; }
       
        public float PriceCar { get; set; }
        public ObservableCollection<BuyCar> _bc = new ObservableCollection<BuyCar>();
        public ObservableCollection<BuyCar> services
        {
            get { return _bc; }
            set
            {
                _bc = value;
                OnPropertyChanged();
            }
        }
            FirebaseClient f = new FirebaseClient("https://travels-a5c41-default-rtdb.firebaseio.com/");
        public async Task<bool> Save(BuyCar b)
        {
            var data = await f.Child(nameof(BuyCar)).PostAsync(JsonConvert.SerializeObject(b));
            if (!string.IsNullOrEmpty(data.Key))
            {

                return true;
            }
            return false;
            
        }
        public async Task<List<BuyCar>> GetallCar()
        {
            return (await f.Child("BuyCar").OnceAsync<BuyCar>()).Select(item =>
             new BuyCar
             {
                    idOfUser=item.Object.idOfUser,
                    ServiceList=item.Object.ServiceList,
                    PriceCar=item.Object.PriceCar

             }).ToList();
        }
        public async Task<BuyCar?> GetCarForId(string id)
        {
            var allp = await GetallCar();
            await f.Child("BuyCar").OnceAsync<BuyCar>();    
            var l= allp.FirstOrDefault(a => a.idOfUser.Equals(id));
            if (l==null)
            {
                return null;
            }
            else
            {
                return l;
            }
        }public async Task UpdateRow(List<Model.Services> bc,string id,int price)
        {
               var update = (await f.Child("BuyCar").OnceAsync<BuyCar>()).Where(a => a.Object.idOfUser == id).FirstOrDefault();
                update.Object.ServiceList = bc;
                update.Object.PriceCar = price;
                await f.Child("BuyCar").Child(update.Key).PutAsync(update.Object);
            Debug.WriteLine("Exito");
            

        }
        public async Task<bool> DeleteFromCar(int? index,string uid)
        {
            BuyCar? todeleteservice = (await GetCarForId(uid));
            for(int i = 0; i < todeleteservice?.ServiceList.Count; i++)
            {
                if (i ==index)
                {
                    List <Services>? s= todeleteservice.ServiceList;
                    todeleteservice.PriceCar = todeleteservice.PriceCar - s[i].Price;
                    float price = todeleteservice.PriceCar;
                    s.RemoveAt(i);
                    await UpdateRow(s, uid, (int)price);
                    return true;
                }

                
            }
            return false;

        }
        }
    

    
    }

