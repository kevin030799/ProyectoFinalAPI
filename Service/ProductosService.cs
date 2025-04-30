using ProyectoFinalAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinalAPI.Service
{
    public class ProductosService
    {
        private readonly string _connectionString;

        public ProductosService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }

        // Obtener todos los productos
        public async Task<List<ProductosModel>> ObtenerProductosAsync()
        {
            var productos = new List<ProductosModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerProductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(new ProductosModel
                            {
                                Id = (int)reader["Id"],
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = (decimal)reader["Precio"],
                                Existencias = (int)reader["Existencias"],
                                FechaAdicion = (DateTime)reader["FechaAdicion"],
                                AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                FechaModificacion = reader["FechaModificacion"] as DateTime?,
                                ModificadoPor = reader["ModificadoPor"]?.ToString()
                            });
                        }
                    }
                }
            }

            return productos;
        }

        // Obtener producto por ID
        public async Task<ProductosModel> ObtenerProductoPorIdAsync(int id)
        {
            ProductosModel producto = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerProductoPorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            producto = new ProductosModel
                            {
                                Id = (int)reader["Id"],
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = (decimal)reader["Precio"],
                                Existencias = (int)reader["Existencias"],
                                FechaAdicion = (DateTime)reader["FechaAdicion"],
                                AdicionadoPor = reader["AdicionadoPor"].ToString(),
                                FechaModificacion = reader["FechaModificacion"] as DateTime?,
                                ModificadoPor = reader["ModificadoPor"]?.ToString()
                            };
                        }
                    }
                }
            }

            return producto;
        }

        // Crear producto
        public async Task CrearProductoAsync(ProductosModel producto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Existencias", producto.Existencias);
                    cmd.Parameters.AddWithValue("@FechaAdicion", producto.FechaAdicion);
                    cmd.Parameters.AddWithValue("@AdicionadoPor", producto.AdicionadoPor);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Actualizar producto
        public async Task<bool> ActualizarProductoAsync(int id, ProductosModel producto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ActualizarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Existencias", producto.Existencias);
                    cmd.Parameters.AddWithValue("@FechaModificacion", producto.FechaModificacion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ModificadoPor", producto.ModificadoPor ?? (object)DBNull.Value);

                    await con.OpenAsync();
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

        // Eliminar producto
        public async Task<bool> EliminarProductoAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EliminarProducto", con))
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
