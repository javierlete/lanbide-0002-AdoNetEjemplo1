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
            using (IDbConnection con = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=mf0966;Integrated Security=True")) //@"Server=localhost\SQLEXPRESS;Database=mf0966;Trusted_Connection=True;");
            {
                con.Open();
                
                MostrarClientes(con);
                
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Clientes WHERE ClienteId = 1";
                using (IDataReader dr = com.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine($"{dr["ClienteId"]}, {dr["Nombre"]}, {dr["Apellidos"]}, {dr["FechaNacimiento"]}");
                    }
                }

                com.CommandText = "INSERT INTO Clientes (Nombre, Apellidos, FechaNacimiento) VALUES ('Nuevo', 'Nuevez', '2000-01-02')";
                com.ExecuteNonQuery();

                MostrarClientes(con);

                com.CommandText = "UPDATE Clientes SET Nombre='Modificado',Apellidos='Modificadez', FechaNacimiento='2002-02-02' WHERE ClienteId=4";
                com.ExecuteNonQuery();

                MostrarClientes(con);

                com.CommandText = "DELETE FROM Clientes WHERE ClienteId = 3";
                com.ExecuteNonQuery();

                MostrarClientes(con);
            } // con.Close();
        }

        private static void MostrarClientes(IDbConnection con)
        {
            IDbCommand com = con.CreateCommand();
            com.CommandText = "SELECT * FROM Clientes";

            using (IDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["ClienteId"]}, {dr["Nombre"]}, {dr["Apellidos"]}, {dr["FechaNacimiento"]}");
                }
            }
        }
    }
}
