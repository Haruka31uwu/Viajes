using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Xamarin.Forms;

namespace Viajes.ViewModels
{
   
    public class UsersRepository
    {
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAXG30e4kls-Cur4MjFm4FuHZIl0TU4b3c"));
        FirebaseClient f = new FirebaseClient("https://travels-a5c41-default-rtdb.firebaseio.com/");
        public async Task<bool> Save(Users u)
        {
            var data = await f.Child(nameof(Users)).PostAsync(JsonConvert.SerializeObject(u));
            if (!string.IsNullOrEmpty(data.Key))
            {

                return true;
            }
            return false;
        }public async Task<bool> Register(string email,string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }public async Task<string> SignIn(string email,string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email,password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            else
            {
                return "";
            }
        }public string  GetUser(string token)
        {
            return  authProvider.GetUserAsync(token).Result.Email;
        }
   
    }
        
    
}
