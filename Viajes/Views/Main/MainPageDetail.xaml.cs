using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Viajes.Views.Main.MainPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public UsersRepository ur;
        public MainPageDetail(Users u)
        {
            InitializeComponent();
            lbl.Text = lbl.Text +u.Email;
        
            
        }
    }
}