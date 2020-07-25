using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin.Models
{
    
    public class Users
    {
        //Define  Model In db
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        
        public String Email { get; set; }


        public string Password { get; set; }


    }
}
