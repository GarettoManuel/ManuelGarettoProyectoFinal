using ManuelGarettoProyectoFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuelGarettoProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //EJERCICIO 1
        [HttpGet("{NombreUsuario}/{Contraseña}")]
        public Usuario LogIn(string nombreUsuario, string contraseña)
        {
            var usuario = ManejadorUsuario.Login(nombreUsuario, contraseña);
            return usuario;
        }


        //EJERCICIO 2
        [HttpPost]
        public Models.Usuario InsertarUsuario(Models.Usuario usuario)
        {
            ManejadorUsuario.InsertarUsuario(usuario);
            return usuario;
        }

        //EJERCICIO 3
        [HttpPut]
        public long ModificarUsuario(Usuario id)
        {
            return ManejadorUsuario.ModificarUsuario(id);
        }


        //EJERCICIO 4
        [HttpGet("{nombreUsuario}")]
        public Models.Usuario DevolverUsuario(string nombreUsuario)
        {
            return ManejadorUsuario.devolverUsuario(nombreUsuario);
        }


        //EJERCICIO 5
        [HttpDelete("{id}")]
        public string EliminarUsuario(long id)
        {
            return ManejadorUsuario.EliminarUsuario(id) == 1 ? "Eliminado" : "No se puede eliminar";
        }

    }
}
