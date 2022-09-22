using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//ya manito
namespace Viajes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
       
        UsersRepository _user = new UsersRepository();
        public Login()
        {
            InitializeComponent();   
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            string password = txtpass.Text;
            bt1.IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {

                bt1.IsEnabled = true;
                return false;
            });

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Rellene bien los campos porfavor", "Ok");
            }
            else
            {
                try
                {

                    string token = await _user.SignIn(email, password);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var u =await _user.UserLogedData(email);
                        Debug.WriteLine(u.Email);
                       await Navigation.PushModalAsync(new Views.Main.MainPage(u));
                       
                    }
                    else
                    {
                        
                        //await DisplayAlert("Error", "SignUp Failed", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    {
                        if (ex.Message.Contains("INVALID_PASSWORD") )
                        {
                            await DisplayAlert("Warning", "Password Incorrect", "Ok");
                            
                        }if (ex.Message.Contains("INVALID_EMAIL")) {
                            await DisplayAlert("Warning", "Invalid Email", "OK");
                        } 
                        else
                        {

                            await DisplayAlert("Error", ex.Message, "Ok");

                        }
                    }
                    
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new RegisterUser());
        }
    }
}