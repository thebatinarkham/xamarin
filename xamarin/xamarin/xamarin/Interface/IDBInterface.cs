using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin.Interface
{
    public interface IDBInterface
    {
        SQLiteConnection createConnection();

        String GetPath();

    }
}
