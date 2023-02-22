using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoProyectoFinal
{
    
    internal static class ManejadorProducto
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //EJERCICIO 6 ------------------------------------------------------------------------------------------------------------
        public static int CrearProducto(Models.Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);

                conn.Open();
                return comando.ExecuteNonQuery();

            }
        }
        //EJERCICIO 6 ------------------------------------------------------------------------------------------------------------





        //EJERCICIO 7 ------------------------------------------------------------------------------------------------------------¡
        public static long ModificarProducto(Models.Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {

                SqlCommand comando = new SqlCommand("UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id", conn);
                comando.Parameters.AddWithValue("@id", producto.Id);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                conn.Open();
                return comando.ExecuteNonQuery();

            }
        }
        //EJERCICIO 7 ------------------------------------------------------------------------------------------------------------





        //EJERCICIO 8 ------------------------------------------------------------------------------------------------------------

        public static int EliminarProducto(long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                try
                {
                    SqlCommand comando = new SqlCommand("DELETE FROM Producto WHERE id=@id", conn);
                    comando.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return -1;
                }
        }

        //EJERCICIO 8 ------------------------------------------------------------------------------------------------------------





        //EJERCICIO 10 ------------------------------------------------------------------------------------------------------------
        public static List<Models.Producto> ObtenerProductos(long id)
        {

            var listaProductos = new List<Models.Producto>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {

                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario=@IdUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", id);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Models.Producto producto = new Models.Producto();

                        producto.Id = reader.GetInt64(0);
                        producto.Descripciones = reader.GetString(1);
                        producto.Costo = reader.GetDecimal(2);
                        producto.PrecioVenta = reader.GetDecimal(3);
                        producto.Stock = reader.GetInt32(4);
                        producto.IdUsuario = reader.GetInt64(5);

                        listaProductos.Add(producto);
                    }
                }
                return listaProductos;
            }
        }
        //EJERCICIO 10 ------------------------------------------------------------------------------------------------------------

    }
}
