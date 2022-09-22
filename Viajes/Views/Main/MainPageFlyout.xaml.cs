using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Viajes.Views.Main.MainPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        public ListView ListView;
        
        public MainPageFlyout()
        {
            InitializeComponent();
            
            BindingContext = new MainPageFlyoutViewModel();
            
            ListView = MenuItemsListView;
            
            
        }

        public class MainPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

            public MainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
                {
                    new MainPageFlyoutMenuItem{ Id = 0, Title = "Destacados",TargetType = typeof(Home),Source = "home.png"},
                    new MainPageFlyoutMenuItem{ Id = 1, Title = "Buscar",TargetType = typeof(Search),Source = "buscar.png"},
                    new MainPageFlyoutMenuItem{Id=2,Title="Carrito",TargetType=typeof(BuyCar),Source="cart.png"},
                    new MainPageFlyoutMenuItem{Id=3,Title="Registro",TargetType=typeof(BuyOrderPage),Source="verify.png"}
                   
                });
                
                
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}