using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using MvvmHelpers;
using MvvmHelpers.Commands;
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
    public class ServicesRepository:BaseViewModel
    {
        public string NameOfService { get; set; }
        public string Destination { get; set; }
        public string Data { get; set; }
        public string ImageName { get; set; }
        public string DestinationCategory { get; set; }
        public ObservableCollection<Services> _services = new ObservableCollection<Services>();
        public Command AddServiceCommand { get;}
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAXG30e4kls-Cur4MjFm4FuHZIl0TU4b3c"));
        FirebaseClient f = new FirebaseClient("https://travels-a5c41-default-rtdb.firebaseio.com/");
        public async Task<List<Services>> GetallServices() {
            return (await f.Child("Services").OnceAsync<Services>()).Select(item =>
             new Services
             {
                    IdOfService=item.Object.IdOfService,
                    NameOfService=item.Object.NameOfService,
                    Destination=item.Object.Destination,
                    Data=item.Object.Data,
                    ImageName=item.Object.ImageName,
                    Priority=item.Object.Priority,
                    DestinationCategory=item.Object.DestinationCategory
             }).ToList();
        }public async Task<List<Services>> GetServiceForCat(string cat)
        {
            var allp = await GetallServices();
            await f.Child("Services").OnceAsync<Services>();
            return allp.Where(a => a.DestinationCategory.Equals(cat)).ToList();
        }
        public async Task<List<Services>> GetServiceForName(string name)
        {
            var allp = await GetallServices();
            await f.Child("Services").OnceAsync<Services>();

            return allp.Where(a => a.NameOfService.ToLower().Contains(name.ToLower())).ToList();
        }
        public async Task<List<Services>> GetallServicesforPriority()
        {
            return (await f.Child("Services").OrderBy("Priority").LimitToLast(2).OnceAsync<Services>()).Select(item => new Services
            {
                IdOfService = item.Object.IdOfService,
                NameOfService = item.Object.NameOfService,
                Destination = item.Object.Destination,
                Data = item.Object.Data,
                ImageName = item.Object.ImageName,
                Priority = item.Object.Priority,
                DestinationCategory=item.Object.DestinationCategory
            }).ToList();
        }
        public async Task AddService(string nof,string des,string dta,string imgname,string dc)
        {
            List<Services> li;
            li = new List<Services>();
            li = await GetallServices();
            Services u = new Services()
            {
                IdOfService = (li.Count()+1).ToString(),
                NameOfService = nof,
                Destination = des,
                Data = dta,
                ImageName = imgname,
                Priority = 0.ToString(),
                DestinationCategory=dc
            };
            
            

            Debug.WriteLine(li.Count());
            await f.Child("Services").PostAsync(u);
        }
        public ObservableCollection<Services> services
        {
            get { return _services; }
            set { _services = value;
                OnPropertyChanged();
            }
        }public ServicesRepository()
        {

            AddServiceCommand = new Command(async () => await AddService(NameOfService, Destination, Data, ImageName, DestinationCategory));
        }

        public static implicit operator List<object>(ServicesRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
