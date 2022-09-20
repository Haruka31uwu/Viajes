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
                        await Navigation.PushAsync(new Login());

                    }
                    else
                    {
                        await DisplayAlert("Error", "Student save failed", "Ok");
                    }
                }catch(Exception ex)
                {
                    if (ex.Message.Contains("EMAIL_EXIST"))
                    {
                        await DisplayAlert("Warning", "Email exist", "Ok");
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