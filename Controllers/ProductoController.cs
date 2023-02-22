using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuelGarettoProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //EJERCICIO 6
        [HttpPost]
        public Models.Producto CrearProducto(Models.Producto producto)
        {
            ManejadorProducto.CrearProducto(producto);
            return producto;
        }

        //EJERCICIO 7
        [HttpPut]
        public string ModificarProducto(Models.Producto id)
        {
            return ManejadorProducto.ModificarProducto(id) == 1 ? "Modificado" : "No se pudo modificar";
        }

        //EJERCICIO 8
        [HttpDelete("{id}")]
        public string EliminarProducto(long id)
        {
            return ManejadorProducto.EliminarProducto(id) == 1 ? "Eliminado" : "No se puede eliminar";
        }



        //EJERCICIO 10
        [HttpGet("{idUsuario}")]
        public List<Models.Producto> ObtenerProducto(long idUsuario)
        {
            return ManejadorProducto.ObtenerProductos(idUsuario);
        }

    }
}
