using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin.Models
{
    public class Products
    {
        //Define  Model In db
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public int UserId { get; set; }

        
    }
}
