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
                com.CommandText = "SELECT * FROM Clientes WHERE ClienteId = @Id";
                
                IDbDataParameter parId = com.CreateParameter();
                parId.ParameterName = "Id";
                parId.DbType = DbType.Int64;
                parId.Value = 1;
                com.Parameters.Add(parId);

                using (IDataReader dr = com.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine($"{dr["ClienteId"]}, {dr["Nombre"]}, {dr["Apellidos"]}, {dr["FechaNacimiento"]}");
                    }
                }

                com.CommandText = "INSERT INTO Clientes (Nombre, Apellidos, FechaNacimiento) VALUES (@Nombre, @Apellidos, @FechaNacimiento)";

                IDbDataParameter parNombre = com.CreateParameter();
                parNombre.ParameterName = "Nombre";
                parNombre.DbType = DbType.String;
                com.Parameters.Add(parNombre);

                IDbDataParameter parApellidos = com.CreateParameter();
                parApellidos.ParameterName = "Apellidos";
                parApellidos.DbType = DbType.String;
                com.Parameters.Add(parApellidos);

                IDbDataParameter parFechaNacimiento = com.CreateParameter();
                parFechaNacimiento.ParameterName = "FechaNacimiento";
                parFechaNacimiento.DbType = DbType.Date;
                com.Parameters.Add(parFechaNacimiento);

                parNombre.Value = "Nuevazo";
                parApellidos.Value = "Nuevecez";
                parFechaNacimiento.Value = DateTime.Now;
                
                com.ExecuteNonQuery();

                MostrarClientes(con);

                com.CommandText = "UPDATE Clientes SET Nombre=@Nombre,Apellidos=@Apellidos, FechaNacimiento=@FechaNacimiento WHERE ClienteId=@Id";

                parId.Value = 4;
                parNombre.Value = "Supermodificado";
                parApellidos.Value = "Supermodificadez";
                parFechaNacimiento.Value = DateTime.Now;

                com.ExecuteNonQuery();

                MostrarClientes(con);

                com.CommandText = "DELETE FROM Clientes WHERE ClienteId = @Id";

                parId.Value = 2;

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
