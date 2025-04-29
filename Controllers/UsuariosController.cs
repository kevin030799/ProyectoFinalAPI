using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAPI.Models;
using ProyectoFinalAPI.Service;

namespace ProyectoFinalAPI.Controllers
{
    // obtener usuarios 
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuarioService;

        public UsuariosController(UsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]

        public async Task<ActionResult<List<UsuariosModel>>> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }
        // obtener usuario por identificador 
        [HttpGet("{id}")]

        public async Task<ActionResult<List<UsuariosModel>>> ObtenerUsuariosPorId()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();

            if (usuarios == null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }


        // validar usuario que autentica 

        [HttpPost("validar")]
        public async Task<ActionResult<UsuariosModel>> ValidarUsuario([FromQuery] string correo, [FromQuery] string clave)
        {
            var usuario = await _usuarioService.ValidarUsuarioAsync(correo, clave);
            if (usuario == null)
            {
                return Unauthorized(); // 401 si no se encuentra el usuario
            }
            return Ok(usuario); // Retorna el usuario si la validación es exitosa
        }

        // crear usuario 

        [HttpPost]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuariosModel usuario)
        {
            await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(ObtenerUsuariosPorId), new { id = usuario.Id }, usuario);

        }
        // actualizar usuario 


        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, [FromBody] UsuariosModel usuario)
        {
            var actualizado = await _usuarioService.ActualizarUsuarioAsync(id, usuario);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        // eliminar usuario 

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuarioAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
