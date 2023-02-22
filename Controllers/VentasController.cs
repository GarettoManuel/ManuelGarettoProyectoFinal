using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuelGarettoProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        //EJERCICIO 9
        [HttpPost("{idUsuario}")]
        public void PostVenta(List<Models.Producto> productos, int idUsuario)
        {
            ManejadorVentas.CargarVenta(idUsuario, productos);
        }
    }
}
