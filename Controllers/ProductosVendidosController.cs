using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuelGarettoProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosVendidosController : ControllerBase
    {
        //EJERCICIO 11
        [HttpGet("{idUsuario}")]
        public List<Models.Producto> GetProductosVenidos(long idUsuario)
        {
            return ManejadorProductosVendidos.ObtenerProductosVendidosId(idUsuario);
        }

    }
}
