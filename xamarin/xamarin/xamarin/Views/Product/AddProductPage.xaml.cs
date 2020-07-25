using Android.Provider;
using PCLStorage;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin.Models;
using xamarin.Views.Signup;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace xamarin.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        private Models.Products product;
        private int ProductKey;
        private string ImageFromGallery = "";

        public AddProductPage()
        {
            InitializeComponent();

            
        }

        public AddProductPage(Products product)
        {

            InitializeComponent();

            if (product != null)
            {
                this.BindingContext = product;

                this.product = product;
                ProductKey = product.Id;

                ImageFromGallery = product.ImageUrl;


            }
            
            
        }

        

        async private void BackToHome_Button(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        
            
        }

        async private void NavToProImage_Clicked(object sender, EventArgs e)
        {
            

            Models.Products product = new Models.Products()
            {
                Id = ProductKey,
                Name = ProductName.Text,
                Price = int.Parse(ProductPrice.Text),
                Description = ProductDes.Text,
                Location = Location.Text,
                Category = ProductCategoryPicker.SelectedItem.ToString(),
                ImageUrl = ImageFromGallery,

                //{ File: / storage / emulated / 0 / Android / data / com.companyname.xamarin / files / Pictures / temp / Screenshot_20191229 - 222154_MortalKombat_4.jpg}
                //"/storage/emulated/0/Android/data/com.companyname.xamarin/files/Pictures/temp/Screenshot_20191229-222154_MortalKombat_4.jpg"


            };

            await Navigation.PushAsync(new AddProductImage(product));

            
        }

         async protected override void OnAppearing()
        {
            base.OnAppearing();




            var location = await Geolocation.GetLastKnownLocationAsync();

            Device.BeginInvokeOnMainThread(() =>
            {
                //ProductCategoryPicker.BackgroundColor = Color.Red;
                //ProductCategoryPicker.TextColor = Color.Pink;


            });

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            }


            try
            {
                var lat = location.Latitude;
                var lon = location.Longitude;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    Location.Text = placemark.SubLocality + ","  + placemark.SubAdminArea;
                }

                

                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Console.WriteLine(geocodeAddress);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                await DisplayAlert("Failed", "FnsEx Error..." + fnsEx.ToString()  , "Okk");
                return;
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                await DisplayAlert("Failed", "Exception Accured.." + ex.ToString(), "Okk");
                return;
            }

            
        }






    }
}