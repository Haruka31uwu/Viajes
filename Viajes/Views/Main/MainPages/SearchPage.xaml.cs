using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.Model;
using Viajes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        ServicesRepository _sr;
        public SearchPage(Task<List<Services>> sr)
        {
            InitializeComponent();
            _sr=new ServicesRepository();
            BindingContext = _sr;

            new Action(async () => ls.ItemsSource = await sr).Invoke();
        }
    }
}