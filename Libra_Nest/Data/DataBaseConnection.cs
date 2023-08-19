using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Libra_Nest.Data
{
    public class DataBaseConnection
    {
        static string dir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Libra_nest_DB.db");
        private static string ConnectionString = $"Data Source={dir};Version=3;";


        public static List<Books> LoadBooks()
        {

            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                var output = cnn.Query<Books>("select * from Books", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void AddBook(Books book)
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Execute($"insert into Books (Title, Author) values ('{book.title}', '{book.author}')");
            }
        }
        

    }
}
