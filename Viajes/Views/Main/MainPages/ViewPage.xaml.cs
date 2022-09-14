using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Views.Pop_Ups;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPage : ContentPage
    {
        public ViewPage(string Ns,string Des,string Data,string ig)
        {
            InitializeComponent();
            img.Source = ig;
            sn.Text = Ns;
            des.Text = Des;
            dt.Text = Data;
            
        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new Date_PopUp());
        }
    }
}