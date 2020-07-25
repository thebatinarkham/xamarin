using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin.Views.Signup
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            
            if(passwordEntry.Text == confirmPasswordEntry.Text)
            {

                try
                {
                    //Register User
                    Users users = new Users()
                    {
                        FirstName = firstNameEntry.Text,
                        LastName = lastNameEntry.Text,
                        Email = emailEntry.Text,
                        Password = passwordEntry.Text
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Users>();

                        int rows = conn.Insert(users);

                        if (rows > 0)
                        {
                            DisplayAlert("Success", "User Successfully Created...", "Ok");
                            Navigation.PushAsync(new Views.Login.LoginPage());
                        }
                        else
                        {
                            DisplayAlert("Failed", "Something Wrong Happen...", "Ok");
                        }
                    }

                    //Insert IN table


                }
                catch
                {

                }


            }
            else
            {
                DisplayAlert("Error", "Password Not Match...", "Ok");
            }

        }

        

        private void NavigateLogin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Login.LoginPage());
        }
    }
}