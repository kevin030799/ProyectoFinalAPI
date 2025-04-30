using ProyectoFinalAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinalAPI.Service
{
    public class ClientesService
    {
            private readonly string _connectionString;

            public ClientesService(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("MiConexion");
            }

            // Obtener todos los clientes
            public async Task<List<ClientesModel>> ObtenerClientesAsync()
            {
                var clientes = new List<ClientesModel>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerClientes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await con.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                clientes.Add(new ClientesModel
                                {
                                    Id = (int)reader["Id"],
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Pais = reader["Pais"].ToString(),
                                    Provincia = reader["Provincia"].ToString(),
                                    Canton = reader["Canton"].ToString(),
                                    Distrito = reader["Distrito"].ToString(),
                                    FechaAdicion = (DateTime)reader["FechaAdicion"],
                                    AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                    FechaModificacion = reader["FechaModificacion"] as DateTime?,
                                    ModificadoPor = reader["ModificadoPor"]?.ToString()
                                });
                            }
                        }
                    }
                }

                return clientes;
            }

            // Obtener cliente por ID
            public async Task<ClientesModel> ObtenerClientePorIdAsync(int id)
            {
                ClientesModel cliente = null;

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerClientes", con)) // suponiendo que filtras dentro del SP
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await con.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if ((int)reader["Id"] == id)
                                {
                                    cliente = new ClientesModel
                                    {
                                        Id = (int)reader["Id"],
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellido = reader["Apellido"].ToString(),
                                        Correo = reader["Correo"].ToString(),
                                        Telefono = reader["Telefono"].ToString(),
                                        Pais = reader["Pais"].ToString(),
                                        Provincia = reader["Provincia"].ToString(),
                                        Canton = reader["Canton"].ToString(),
                                        Distrito = reader["Distrito"].ToString(),
                                        FechaAdicion = (DateTime)reader["FechaAdicion"],
                                        AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                        FechaModificacion = reader["FechaModificacion"] as DateTime?,
                                        ModificadoPor = reader["ModificadoPor"]?.ToString()
                                    };
                                    break;
                                }
                            }
                        }
                    }
                }

                return cliente;
            }

            // Crear nuevo cliente
            public async Task CrearClienteAsync(ClientesModel cliente)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Pais", cliente.Pais);
                        cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                        cmd.Parameters.AddWithValue("@Canton", cliente.Canton);
                        cmd.Parameters.AddWithValue("@Distrito", cliente.Distrito);
                        cmd.Parameters.AddWithValue("@AdicionadoPor", cliente.AdicionadoPor);

                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            // Actualizar cliente
            public async Task<bool> ActualizarClienteAsync(int id, ClientesModel cliente)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ActualizarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Pais", cliente.Pais);
                        cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                        cmd.Parameters.AddWithValue("@Canton", cliente.Canton);
                        cmd.Parameters.AddWithValue("@Distrito", cliente.Distrito);
                        cmd.Parameters.AddWithValue("@ModificadoPor", cliente.ModificadoPor);

                        await con.OpenAsync();
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }

            // Eliminar cliente
            public async Task<bool> EliminarClienteAsync(int id)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("EliminarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        await con.OpenAsync();
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
        }

}
