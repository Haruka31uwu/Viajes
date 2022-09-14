using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
