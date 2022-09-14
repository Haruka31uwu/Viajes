using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayPage : ContentPage
    {
        public PayPage()
        {
            InitializeComponent();
        }

        private void btnConfirmarPago_Clicked(object sender, EventArgs e)
        {

        }
    }
}