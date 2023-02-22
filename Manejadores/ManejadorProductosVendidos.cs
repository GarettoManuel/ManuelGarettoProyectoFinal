using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoProyectoFinal
{
    internal static class ManejadorProductosVendidos
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //EJERCICIO 11 ------------------------------------------------------------------------------------------------------------
        public static void InsertarProductoVendido(Models.ProductoVendido productoVendido)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                var query = @"INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta)";

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.AddWithValue("@Stock", productoVendido.Stock);
                comando.Parameters.AddWithValue("@IdProducto", productoVendido.IdProducto);
                comando.Parameters.AddWithValue("@IdVenta", productoVendido.IdVenta);

                conn.Open();
                comando.ExecuteNonQuery();

            }
        }

        
        public static List<Models.Producto> ObtenerProductosVendidosId(long idUsuario)
        {
            List<Models.Producto> productos = new List<Models.Producto>();

            using (SqlConnection conection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto INNER JOIN ProductoVendido ON Producto.Id = ProductoVendido.IdProducto INNER JOIN Venta ON Venta.Id = ProductoVendido.IdVenta WHERE Venta.IdUsuario = @IdUsuario", conection);

                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conection.Open();

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
                        productos.Add(producto);
                    }
                }
                return productos;
            }
        }

        //EJERCICIO 11 ------------------------------------------------------------------------------------------------------------
    }
}
