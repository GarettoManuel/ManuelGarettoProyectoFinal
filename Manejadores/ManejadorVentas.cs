using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoProyectoFinal
{
    internal static class ManejadorVentas
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //EJERCICIO 9 ------------------------------------------------------------------------------------------------------------

        public static long InsertarVenta(Models.Venta venta)
        {

            using (SqlConnection conection = new SqlConnection(cadenaConexion))
            {
                var query = "INSERT INTO Venta (Comentarios, IdUsuario) VALUES(@Comentarios, @IdUsuario); SELECT @@IDENTITY";
                SqlCommand comando = new SqlCommand(query, conection);

                comando.Parameters.AddWithValue("@Comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);

                conection.Open();

                return Convert.ToInt64(comando.ExecuteScalar());
            }
        }

        public static void CargarVenta(long idUsuario, List<Models.Producto> productosVendidos)
        {
            Models.Venta venta = new Models.Venta();
            using (SqlConnection conection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand();
                conection.Open();

                venta.Comentarios = "";
                venta.IdUsuario = idUsuario;
                venta.Id = InsertarVenta(venta);

                foreach (Models.Producto producto in productosVendidos)
                {
                    Models.ProductoVendido productoVendido = new Models.ProductoVendido();
                    productoVendido.Stock = producto.Stock;
                    productoVendido.IdProducto = producto.Id;
                    productoVendido.IdVenta = venta.Id;

                    ManejadorProductosVendidos.InsertarProductoVendido(productoVendido);

                    ActualizarStock(productoVendido.IdProducto, productoVendido.Stock);

                }

            }
        }


        public static Models.Producto ObtenerProducto(long id)
        {
            Models.Producto producto = new Models.Producto();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {

                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE @Id=id", conn);
                comando.Parameters.AddWithValue("@Id", id);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);

                }
                return producto;
            }
        }


        public static Models.Producto ActualizarProducto(Models.Producto producto)
        {

            using (SqlConnection conn = new SqlConnection(cadenaConexion))

            {
                SqlCommand comando = new SqlCommand(@"UPDATE Producto SET [Descripciones]= @Descripciones, [Costo]= @Costo, [PrecioVenta]= @PrecioVenta, [Stock]=@Stock WHERE [Id]=@Id", conn);
                conn.Open();
                comando.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@Costo", producto.Costo);
                comando.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@Id", producto.Id);
                comando.ExecuteNonQuery();
                
            }

            return producto;
        }



        public static Models.Producto ActualizarStock(long id, int cantidadVendidos)
        {
            Models.Producto producto = ObtenerProducto(id);
            producto.Stock -= cantidadVendidos;
            return ActualizarProducto(producto);
        }

        //EJERCICIO 9 ------------------------------------------------------------------------------------------------------------

    }

}

