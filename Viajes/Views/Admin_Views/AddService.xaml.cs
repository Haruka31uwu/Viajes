using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Viajes.Views.Admin_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddService : ContentPage
    {
        MediaFile file;
        ServicesRepository sr;
        FirebaseStorage fr;
        public string Image;    
        
        public AddService()
        {
            InitializeComponent();
            sr = new ServicesRepository();
            fr = new FirebaseStorage("travels-a5c41.appspot.com");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });   
                Debug.WriteLine(imgChoosed.Source);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task<string> StoreImages(Stream imageStream,string file)
        {            
            var stroageImage = await fr
                .Child("Services_Images")
                .Child(file)
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            Image = imgurl.ToString();
            return imgurl;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try {
               
                await StoreImages(file.GetStream(), Path.GetFileName(file.Path));
                await sr.AddService(ns.Text, ds.Text, das.Text,Image.ToString(),dc.SelectedItem.ToString());
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
             await DisplayAlert("Exito", "Servicio añadido con exito", "Ok");
        }
    }
}