using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI.Models;
using ProyectoFinalAPI.Service;

namespace ProyectoFinalAPI.Controllers
{
    public class ClientesController : Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ClienteController : Controller
        {
            private readonly ClientesService _clienteService;

            public ClienteController(ClientesService clienteService)
            {
                _clienteService = clienteService;
            }

            // Obtener todos los clientes
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ClientesModel>>> ObtenerClientes()
            {
                var clientes = await _clienteService.ObtenerClientesAsync();
                return Ok(clientes);
            }

            // Obtener cliente por ID
            [HttpGet("{id}")]
            public async Task<ActionResult<ClientesModel>> ObtenerClientePorId(int id)
            {
                var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
                if (cliente == null)
                    return NotFound("Cliente no encontrado");
                return Ok(cliente);
            }

            // Crear nuevo cliente
            [HttpPost]
            public async Task<ActionResult> CrearCliente([FromBody] ClientesModel cliente)
            {
                await _clienteService.CrearClienteAsync(cliente);
                return Ok("Cliente agregado correctamente");
            }

            // Actualizar cliente
            [HttpPut("{id}")]
            public async Task<ActionResult> ActualizarCliente(int id, [FromBody] ClientesModel cliente)
            {
                var resultado = await _clienteService.ActualizarClienteAsync(id, cliente);
                if (!resultado)
                    return NotFound("Cliente no encontrado");
                return Ok("Cliente actualizado correctamente");
            }

            // Eliminar cliente
            [HttpDelete("{id}")]
            public async Task<ActionResult> EliminarCliente(int id)
            {
                var resultado = await _clienteService.EliminarClienteAsync(id);
                if (!resultado)
                    return NotFound("Cliente no encontrado");
                return Ok("Cliente eliminado");
            }
        }

    }
}
