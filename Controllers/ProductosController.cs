using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI.Models;
using ProyectoFinalAPI.Service;

namespace ProyectoFinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private readonly ProductosService _productoService;

        public ProductosController(ProductosService productoService)
        {
            _productoService = productoService;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductosModel>>> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerProductosAsync();
            return Ok(productos);
        }

        // Obtener producto por identificador
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductosModel>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado");
            return Ok(producto);
        }

        // Crear producto
        [HttpPost]
        public async Task<ActionResult> CrearProducto([FromBody] ProductosModel producto)
        {
            await _productoService.CrearProductoAsync(producto);
            return Ok("Producto agregado correctamente");
        }

        // Actualizar producto
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProducto(int id, [FromBody] ProductosModel producto)
        {
            var resultado = await _productoService.ActualizarProductoAsync(id, producto);
            if (!resultado)
                return NotFound("Producto no encontrado");
            return Ok("Producto actualizado correctamente");
        }

        // Eliminar producto
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var resultado = await _productoService.EliminarProductoAsync(id);
            if (!resultado)
                return NotFound("Producto no encontrado");
            return Ok("Producto eliminado correctamente");
        }
    }
}
