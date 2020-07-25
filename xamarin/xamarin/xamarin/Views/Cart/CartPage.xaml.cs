using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin.Interface;
using xamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin.Views.Cart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {

        public int CartTotal = 0;       
        List<ProductPropety> ProductInCart = new List<ProductPropety>() ;

        public CartPage()
        {
            InitializeComponent();
           
            
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing(); 

            

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                

               var cartsTable = conn.Table<ShoppingCart>();

                conn.CreateTable<ShoppingCart>();

                var carts =
                    conn.Table<ShoppingCart>().Where(i => i.UserId == App.user.Id).Distinct().ToList();

                var ProductId = (from p in cartsTable where p.UserId == App.user.Id select p.ProductId).Distinct().ToList();

                

                for (int i = 0; i < carts.Count; i++)
                {
                    var productName =
                        (from p in cartsTable where p.UserId == App.user.Id select p.Name).ToList();

                    var productPrice =
                        (from p in cartsTable where p.UserId == App.user.Id select p.Price).ToList();

                    var productQty =
                        (from p in cartsTable where p.UserId == App.user.Id select p.Quantity).ToList();

                    var productImg =
                        (from p in cartsTable where p.UserId == App.user.Id select p.ImageUrl).ToList();

                    ProductInCart.Add(new ProductPropety { Name = productName[i],Price = productPrice[i],Quantity = productQty[i] ,Amount = productPrice[i] * productQty[i],ImageUrl = productImg[i] });

                    CartTotal += ProductInCart[i].Amount;

                   



                }

                cartTotal.Text = "$ " + CartTotal;
                ProductView.ItemsSource = ProductInCart;


            }
        }

        private void PaymentBtn_Clicked(object sender, EventArgs e)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                
                var cartsTable = conn.Table<ShoppingCart>();

                conn.CreateTable<ShoppingCart>();

                var del = conn.Table<ShoppingCart>().Where(i => i.UserId == App.user.Id).Delete();
            }




            Navigation.PopToRootAsync();
        }
    }

    internal class ProductPropety
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int Amount { get; set; }


    }
}