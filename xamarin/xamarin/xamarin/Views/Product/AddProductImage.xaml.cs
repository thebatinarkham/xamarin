using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddProductImage : ContentPage
    {
        
        private Models.Products products;
        private string selectedImageSource;

        public AddProductImage()
        {
            InitializeComponent();
        }


        public AddProductImage(Models.Products product)
        {
            InitializeComponent();

            if(product != null)
            {
                products = product;
                BindingContext = product;

                selectedImage.Source = product.ImageUrl;

                selectedImageSource = product.ImageUrl;



            }



        }



        public async void SelectImageButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

        
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This Is not supported on Your Device", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium //Change from very large
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            //var Gallary = await CrossMedia.Current.;

            if (selectedImageFile == null && string.IsNullOrWhiteSpace(products.ImageUrl))
            {
                await DisplayAlert("Error", "Select Image To Upload...", "Ok");
                
                return;
            }
            

            if (string.IsNullOrWhiteSpace(products.ImageUrl)) {
                 ImageSource.FromStream(() => selectedImageFile.GetStream());


                selectedImageSource = selectedImageFile.Path;
                var sqlImageAlbumPath = selectedImageFile.AlbumPath;

                UpdateAndNavigate();
            }

            if (!string.IsNullOrWhiteSpace(products.ImageUrl))
            {
                if(selectedImageFile != null)
                {
                    ImageSource.FromStream(() => selectedImageFile.GetStream());


                    selectedImageSource = selectedImageFile.Path;

                    var sqlImageAlbumPath = selectedImageFile.AlbumPath;

                    UpdateAndNavigate();
                }
                else
                {
                    selectedImageSource = products.ImageUrl;
                    UpdateAndNavigate();
                }
                


                
            }
            


            
        }

        private void CreatePclStorage()
        {
            ////Create folder -pcl storage

            //string rootPath = Android.App.Application.Context.GetExternalFilesDir(null).ToString();

            //var filePathDir = Path.Combine(rootPath, "batin");

            // if (!File.Exists(filePathDir))
            //{
            //    Directory.CreateDirectory(filePathDir);
            //}
        }

        async private void UpdateAndNavigate()
        {
            try
            {
                Models.Products product = new Models.Products()
                {
                    Id = products.Id,
                    Name = products.Name,
                    Price = products.Price,
                    Description = products.Description,
                    Location = products.Location,
                    Category = products.Category,
                    UserId = App.user.Id,
                    ImageUrl = selectedImageSource,


                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Models.Products>();

                    if(product.Id == 0)
                    {
                        int rows = conn.Insert(product);

                        if (rows > 0)
                        {
                            await DisplayAlert("Success", "Product Added..", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Failed", "Something Wrong Happen", "Ok");
                        }
                    }
                    else
                    {
                        conn.Update(product);
                        
                        
                        //String selectQuery = "UPDATE Products SET Name ="+ product.Name    + " WHERE " + "Id= " + product.Id ;

                        //conn.Execute(selectQuery);


                        //string sql =
                        //    $"UPDATE Products SET Name='{ products.Name }',Price='{ products.Price}',Description='{products.Description}'," +
                        //    $"Location='{products.Location}',Category='{products.Category}',UserId='{App.user.Id}'," +
                        //    $"WHERE Id ='{ products.Id}'";                        

                        //conn.Execute(sql);
                        

                    }

                    //var products = (conn.Table<Models.Products>().Where(p => p.Id == product.Id).FirstOrDefault());


                }

            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failed", nre.ToString(), "Ok");
            }

            catch (Exception ex)
            {
                await DisplayAlert("Failed", ex.ToString(), "Ok");
            };

            await Navigation.PopToRootAsync();
        }
    }
}