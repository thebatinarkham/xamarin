using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin.Models;
using xamarin.Views.Signup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var assembly = typeof(LoginPage);

            //iconImages.Source = ImageSource.FromResource("xamarin.Assets.Images.login.png", assembly);
            
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async private void LoginButton_Clicked(object sender, EventArgs e)
        {
            //var user = 

            

            bool IsEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool IsPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (IsEmailEmpty  || IsPasswordEmpty )
            {
                
            }
            else
            {
                

                //Using statement Call Dispose method from IDisposable Interface autometically
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    //Using(#variabletousing)
                    //{#variabletousing exist in scope only};
                    //Dispose method call Automatically In end

                     

                    //READ TABLE and Converting To List
                    var users = (conn.Table<Users>().Where(u => u.Email == EmailEntry.Text).ToList()).FirstOrDefault();

                    if(users != null)
                    {
                        //User Asigin To App user for available to all class
                        App.user = users;

                        if(users.Password == PasswordEntry.Text)
                        {
                            await Navigation.PushAsync(new Product.ProductMallViewPage());
                        }
                        else
                        {
                            await DisplayAlert("Error","Email or Password are incorrect...","Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "There is error Loging User...", "Ok");
                    }



                 



                }
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Using statement Call Dispose method from IDisposable Interface autometically
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Using(#variabletousing)
                //{#variabletousing exist in scope only};
                //Dispose method call Automatically In end

                //CREATING TABLE WHEN NAVIGATE OR OVERRIDE EXIST TABLE IN NEW PRODUCT PAGE
                conn.CreateTable<Users>();

                //READ TABLE and Converting To List
                var users = conn.Table<Users>();

                



            }
        }
    }
}