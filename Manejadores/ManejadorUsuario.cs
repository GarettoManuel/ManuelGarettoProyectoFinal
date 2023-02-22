using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoProyectoFinal
{
    internal static class ManejadorUsuario
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        //EJERCICIO 1 ------------------------------------------------------------------------------------------------------------
        public static Models.Usuario Login(string nombreUsuario, string contraseña)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña", conn);

                SqlParameter nombre = new SqlParameter();
                nombre.ParameterName = "nombreUsuario";
                nombre.SqlValue = SqlDbType.VarChar;
                nombre.Value = nombreUsuario;

                SqlParameter contraseñaUsuario = new SqlParameter();
                contraseñaUsuario.ParameterName = "contraseña";
                contraseñaUsuario.SqlValue = SqlDbType.VarChar;
                contraseñaUsuario.Value = contraseña;

                comando.Parameters.Add(nombre);
                comando.Parameters.Add(contraseñaUsuario);
                conn.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Models.Usuario usuarioEncontrado = new Models.Usuario();
                        reader.Read();
                        usuarioEncontrado.Id = reader.GetInt64(0);
                        usuarioEncontrado.Nombre = reader.GetString(1);
                        usuarioEncontrado.Apellido = reader.GetString(2);
                        usuarioEncontrado.NombreUsuario = reader.GetString(3);
                        usuarioEncontrado.Contraseña = reader.GetString(4);
                        usuarioEncontrado.Mail = reader.GetString(5);
                        return usuarioEncontrado;
                    }
                }
                return null;
            }
        }
        //EJERCICIO 1 ------------------------------------------------------------------------------------------------------------



        //EJERCICIO 2 ------------------------------------------------------------------------------------------------------------
        public static int InsertarUsuario(Models.Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)", conn);
 
                SqlParameter nombre = new SqlParameter("nombre", usuario.Nombre);
                SqlParameter apellido = new SqlParameter("apellido", usuario.Apellido);
                SqlParameter nombreUsuario = new SqlParameter("nombreUsuario", usuario.NombreUsuario);
                SqlParameter contraseña = new SqlParameter("contraseña", usuario.Contraseña);
                SqlParameter mail = new SqlParameter("mail", usuario.Mail);

                comando.Parameters.Add(nombre);
                comando.Parameters.Add(apellido);
                comando.Parameters.Add(nombreUsuario);
                comando.Parameters.Add(contraseña);
                comando.Parameters.Add(mail);

                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }
        //EJERCICIO 2 ------------------------------------------------------------------------------------------------------------




        //EJERCICIO 3 ------------------------------------------------------------------------------------------------------------
        public static long ModificarUsuario(Models.Usuario usuario)
        {

            using (SqlConnection conn = new SqlConnection(cadenaConexion))

            {
                SqlCommand comando = new SqlCommand("UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id", conn);
                comando.Parameters.AddWithValue("@id", usuario.Id);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }
        //EJERCICIO 3 ------------------------------------------------------------------------------------------------------------





        //EJERCICIO 4 ------------------------------------------------------------------------------------------------------------
        public static Models.Usuario devolverUsuario(string nombre)
        {


            Models.Usuario usuario = new Models.Usuario();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario=@nombreUsuario", conn);
                SqlParameter idParam = new SqlParameter();
                idParam.Value = nombre;
                idParam.SqlDbType = SqlDbType.VarChar;
                idParam.ParameterName = "nombreUsuario";

                comando.Parameters.Add(idParam);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }

            }
            return usuario;
        }
        //EJERCICIO 4 ------------------------------------------------------------------------------------------------------------




        //EJERCICIO 5 ------------------------------------------------------------------------------------------------------------
        public static int EliminarUsuario(long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                try
                {
                    SqlCommand comando = new SqlCommand("DELETE FROM Usuario WHERE id=@id", conn);
                    comando.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return -1;
                }
        }
        //EJERCICIO 5 ------------------------------------------------------------------------------------------------------------

    }
}
