using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;

namespace Viajes.ViewModels
{
 
    public class CarViewModel

    {
        

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
        public async Task<BuyCar> GetCarForId(string id)
        {
            var allp = await GetallCar();
            await f.Child("BuyCar").OnceAsync<BuyCar>();
            return allp.Where(a => a.idOfUser.Equals(id)).First();
        }public async Task UpdateRow(List<Model.Services> bc,string id,int price)
        {
               var update = (await f.Child("BuyCar").OnceAsync<BuyCar>()).Where(a => a.Object.idOfUser == id).FirstOrDefault();
                update.Object.ServiceList = bc;
                update.Object.PriceCar = price;
                await f.Child("BuyCar").Child(update.Key).PutAsync(update.Object);
            Debug.WriteLine("Exito");
            

        }
    }
}
