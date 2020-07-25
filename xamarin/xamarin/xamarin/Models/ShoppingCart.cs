using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin.Models
{
    public class ShoppingCart
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        public int UserId { get; set; }
    }
}
