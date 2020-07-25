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
    public partial class ProductDetails : ContentPage
    {
        
        private Products Product;               


        public ProductDetails()
        {
            InitializeComponent();
        }

        public ProductDetails(Models.Products product)
        {
            InitializeComponent();
            this.BindingContext = product;            
            

            this.Product = product;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<ShoppingCart>();

                var cartQty = conn.Table<ShoppingCart>();                

                var productQtyFromCart = (from p in cartQty where p.UserId == App.user.Id where p.ProductId == Product.Id select p.Quantity).Distinct().ToList();

                if(productQtyFromCart.Count != 0)
                {
                    productQty.Value = productQtyFromCart[0];
                }
                

                
            }

        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<ShoppingCart>();

                

                var cart =
                    conn.Table<ShoppingCart>().Where(i => i.UserId == App.user.Id).ToList();

                

                var productInCart =
                    conn.Table<ShoppingCart>().Where(p => p.ProductId == Product.Id).Where(i => i.UserId == App.user.Id).ToList();


                try
                {
                    if (productInCart.Count == 0)
                    {
                        Models.ShoppingCart carts = new Models.ShoppingCart()
                        {
                            ProductId = Product.Id,
                            Name = Product.Name,
                            Price = Product.Price,
                            UserId = App.user.Id,
                            ImageUrl = Product.ImageUrl,
                            Quantity = int.Parse(productQty.Value.ToString())
                        };
                        conn.Insert(carts);
                    }
                    else
                    {
                        var cartTable = conn.Table<ShoppingCart>().ToList();

                        var shoppingCartId = (from p in cartTable where p.UserId == App.user.Id where p.ProductId == Product.Id select p.Id).Distinct().ToList();
                        
                        
                        Models.ShoppingCart carts = new Models.ShoppingCart()
                        {
                            Id = shoppingCartId[0],
                            ProductId = Product.Id,
                            Name = Product.Name,
                            Price = Product.Price,
                            UserId = App.user.Id,
                            ImageUrl = Product.ImageUrl,
                            Quantity = int.Parse(productQty.Value.ToString())
                        };

                        conn.Update(carts);

                    }
                    
                }
                catch 
                {
                    

                }



            }

            Navigation.PushAsync(new Cart.CartPage());   
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Price.Text = "$" +  (productQty.Value * Product.Price).ToString();
            
        }

        
    }
}