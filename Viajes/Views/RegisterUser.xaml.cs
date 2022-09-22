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

namespace Viajes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        UsersRepository rep=new UsersRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            string email = txtemail.Text;
            string pass = txtpass.Text;
            bt1.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                bt1.IsEnabled = true;
                return false;
            });
            if (string.IsNullOrEmpty(email))
            {
               await DisplayAlert("Warning", "Please Enter your Email", "Cancel");
            }
            if (string.IsNullOrEmpty(pass)|| pass.Replace(" ","").Length<6)
            {
               await DisplayAlert("Warning", "Please Enter a valid password with at least 6 digits", "Cancel");
            }
            else {
                Users u = new Users();
                u.Email = email.ToLower();
                u.Password = pass;
                u.Id = (await  rep.GetcountUsers() + 1).ToString(); 
                
                try
                {
                    var isSaved =
                         await rep.Register(email, pass); 
                    if (isSaved)
                    {
                        Debug.WriteLine(isSaved);

                        await rep.Save(u);
                        await DisplayAlert("Bienvenido", "Usuario Registrado con exito", "Ok");
                        await Navigation.PushAsync(new Login());

                    }
                    else
                    {
                        await DisplayAlert("Error", "Student save failed", "Ok");
                    }
                }catch(Exception ex)
                {
                    if (ex.Message.Contains("EMAIL_EXISTS"))
                    {
                        await DisplayAlert("Warning", "Email exist", "Ok");
                    }
                    else if (ex.Message.Contains("INVALID_EMAIL"))
                    {
                        await DisplayAlert("Warning", "Invalid Email", "OK");
                    }
                    else if (ex.Message.Contains("MISSING_EMAIL"))
                    {
                        await DisplayAlert("Warning", "Blank Email", "OK");
                    }else if (ex.Message.Contains("EMAIL_NOT_FOUND")) 
                    {
                        await DisplayAlert("Warning", "Email not Exists", "OK");
                    }
                    
                    else
                    {
                        await DisplayAlert("Error", ex.Message, "Ok");
                    }
                }
                
            }
            
        }
    }
}