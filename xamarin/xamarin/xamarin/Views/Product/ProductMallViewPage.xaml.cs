using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductMallViewPage : ContentPage
    {        
        public ProductMallViewPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var posts = conn.Table<Products>();

                conn.CreateTable<Products>();

                var product = conn.Table<Products>().ToList();



                CollectionItem.ItemsSource = product;



            }
        }

        

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            await Navigation.PushAsync(new AddProductPage());
            //await Navigation.PushModalAsync(new NavigationPage(new AddProductPage()));
        }

        async private void CollectionItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection[0] as Products ;

            if(App.user.Email == "admin@gmail.com")
            {
                await Navigation.PushAsync(new AddProductPage(product));
                return;
            }

            await Navigation.PushAsync(new ProductDetails(product));
        }
    }
}