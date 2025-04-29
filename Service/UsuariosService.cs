using ProyectoFinalAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinalAPI.Service
{
    public class UsuariosService
    {
        private readonly string _connectionString;

        public UsuariosService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }
        //obtener usuarios 

        public async Task<List<UsuariosModel>> ObtenerUsuariosAsync()
        {
            var usuarios = new List<UsuariosModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerUsuarios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usuarios.Add(new UsuariosModel
                            {
                                Id = (int)reader["Id"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                TipoUsuario = reader["TipoUsuario"].ToString(),
                                Clave = reader["Clave"].ToString()
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        // obtener usuario por identificador


        public async Task<UsuariosModel> ObtenerUsuarioPorIdAsync(int Id)
        {
            UsuariosModel usuario = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerUsuarios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if ((int)reader["Id"] == Id)
                            {
                                usuario = new UsuariosModel
                                {
                                    Id = (int)reader["Id"],
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    TipoUsuario = reader["TipoUsuario"].ToString(),
                                    Clave = reader["Clave"].ToString()
                                };
                                break;
                            }
                        }
                    }
                }
            }
            return usuario;

        }

        // crear usuasrio 
        public async Task CrearUsuarioAsync(UsuariosModel usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // actualziar usuario 

        public async Task<bool> ActualizarUsuarioAsync(int id, UsuariosModel usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ActualizarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0; // true si se actualizó, false si no se encontró
                }
            }
        }

        // eliminar usuario

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0; // true si se eliminó, false si no se encontró
                }
            }
        }
        // usuarioa autentica 

        public async Task<UsuariosModel> ValidarUsuarioAsync(string correo, string clave)
        {
            UsuariosModel usuario = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ValidarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Clave", clave);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new UsuariosModel
                            {
                                Id = (int)reader["Id"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                TipoUsuario = reader["TipoUsuario"].ToString(),
                                Clave = reader["Clave"].ToString()
                            };
                        }
                    }
                }
            }
            return usuario; // Retorna null si no se encuentra el usuario
        }
    }
}
