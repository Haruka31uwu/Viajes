using Firebase.Database;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;

namespace Viajes.ViewModels
{
    public class BuyOrderRepository:BaseViewModel
    {
        public string UserId { get; set; }
        public string IdOrder { get; set; }
        public BuyCar BuyCarOrderElement { get; set; }            
        public string PriceOrder { get; set; }
        FirebaseClient f = new FirebaseClient("https://travels-a5c41-default-rtdb.firebaseio.com/");
        public ObservableCollection<BuyOrder> _bo = new ObservableCollection<BuyOrder>();
        public ObservableCollection<BuyOrder> buyorder
        {
            get { return _bo; }
            set
            {
                _bo = value;
                OnPropertyChanged();
            }
        }
            public async Task<bool> Save(BuyOrder bo)
        {
            var data = await f.Child(nameof(BuyOrder)).PostAsync(JsonConvert.SerializeObject(bo));
            if (!string.IsNullOrEmpty(data.Key))
            {

                return true;
            }
            return false;

        }
        public async Task<List<BuyOrder>> GetallOrder()
        {
            return (await f.Child("BuyOrder").OnceAsync<BuyOrder>()).Select(item =>
             new BuyOrder
             {
                 UserId=item.Object.UserId,
                 IdOrder=item.Object.IdOrder,
                 ServiceOrderElement=item.Object.ServiceOrderElement,
                 PriceOrder=item.Object.PriceOrder


             }).ToList();
        }
        public async Task<List<BuyOrder?>?> GetOrderForId(string id)
        {
            var allp = await GetallOrder();
            //await f.Child("BuyOrder").OnceAsync<BuyOrder>();
            var l = allp.Where(a => a.UserId == id).ToList();
            if (l == null)
            {
                return null;
            }
            else
            {
                return l;
            }
        }
    
    }
   
}

    

