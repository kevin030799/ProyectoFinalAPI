using ProyectoFinalAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinalAPI.Service
{
    public class VentasService
    {
        private readonly string _connectionString;

        public VentasService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }

        // Obtener todas las ventas
        public async Task<List<VentasModel>> ObtenerVentasAsync()
        {
            var ventas = new List<VentasModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerVentas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ventas.Add(new VentasModel
                            {
                                Id = (int)reader["Id"],
                                IdCliente = (int)reader["IdCliente"],
                                IdProducto = (int)reader["IdProducto"],
                                Cantidad = (int)reader["Cantidad"],
                                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                                Total = (decimal)reader["Total"],
                                FechaAdicion = (DateTime)reader["FechaAdicion"],
                                AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                FechaModificacion = reader["FechaModificacion"] == DBNull.Value ? null : (DateTime?)reader["FechaModificacion"],
                                ModificadoPor = reader["ModificadoPor"]?.ToString()
                            });
                        }
                    }
                }
            }

            return ventas;
        }

        // Obtener venta por ID
        public async Task<VentasModel?> ObtenerVentaPorIdAsync(int id)
        {
            VentasModel? venta = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerVentaPorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            venta = new VentasModel
                            {
                                Id = (int)reader["Id"],
                                IdCliente = (int)reader["IdCliente"],
                                IdProducto = (int)reader["IdProducto"],
                                Cantidad = (int)reader["Cantidad"],
                                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                                Total = (decimal)reader["Total"],
                                FechaAdicion = (DateTime)reader["FechaAdicion"],
                                AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                FechaModificacion = reader["FechaModificacion"] == DBNull.Value ? null : (DateTime?)reader["FechaModificacion"],
                                ModificadoPor = reader["ModificadoPor"]?.ToString()
                            };
                        }
                    }
                }
            }

            return venta;
        }

        // Crear nueva venta
        public async Task CrearVentaAsync(VentasModel venta)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertarVenta", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProducto", venta.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", venta.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Total", venta.Total);
                    cmd.Parameters.AddWithValue("@AdicionadoPor", venta.AdicionadoPor);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Actualizar venta existente
        public async Task<bool> ActualizarVentaAsync(int id, VentasModel venta)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ActualizarVenta", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProducto", venta.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", venta.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Total", venta.Total);
                    cmd.Parameters.AddWithValue("@ModificadoPor", venta.ModificadoPor);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

        // Eliminar venta
        public async Task<bool> EliminarVentaAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarVenta", con))
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
