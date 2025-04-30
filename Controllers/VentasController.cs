using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI.Models;
using ProyectoFinalAPI.Service;

namespace ProyectoFinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private readonly VentasService _ventaService;

        public VentasController(VentasService ventaService)
        {
            _ventaService = ventaService;
        }

        // Obtener todas las ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentasModel>>> ObtenerVentas()
        {
            var ventas = await _ventaService.ObtenerVentasAsync();
            return Ok(ventas);
        }

        // Obtener una venta por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<VentasModel>> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaService.ObtenerVentaPorIdAsync(id);
            if (venta == null)
                return NotFound("Venta no encontrada");
            return Ok(venta);
        }

        // Crear una nueva venta
        [HttpPost]
        public async Task<ActionResult> CrearVenta([FromBody] VentasModel venta)
        {
            await _ventaService.CrearVentaAsync(venta);
            return Ok("Venta creada correctamente");
        }

        // Actualizar una venta existente
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarVenta(int id, [FromBody] VentasModel venta)
        {
            var resultado = await _ventaService.ActualizarVentaAsync(id, venta);
            if (!resultado)
                return NotFound("Venta no encontrada");
            return Ok("Venta actualizada correctamente");
        }

        // Eliminar una venta
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarVenta(int id)
        {
            var resultado = await _ventaService.EliminarVentaAsync(id);
            if (!resultado)
                return NotFound("Venta no encontrada");
            return Ok("Venta eliminada correctamente");
        }
    }
