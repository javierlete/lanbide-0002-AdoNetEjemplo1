using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace AdoNetEjemplo1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=mf0966;Integrated Security=True"); //@"Server=localhost\SQLEXPRESS;Database=mf0966;Trusted_Connection=True;");
            IDbCommand com = con.CreateCommand();
            com.CommandText = "SELECT * FROM Clientes";
            con.Open();
            IDataReader dr = com.ExecuteReader();
            
            while(dr.Read())
            {
                Console.WriteLine($"{dr["ClienteId"]}, {dr["Nombre"]}, {dr["Apellidos"]}, {dr["FechaNacimiento"]}");
            }
        }
    }
}
