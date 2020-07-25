using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Login,
        Product,
        SignUpPage
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
