namespace ProyectoFinalAPI.Models
{
    public class VentasModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        // Auditoría
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }

        // Relaciones
        public ClientesModel Cliente { get; set; } = null!;
        public ProductosModel Producto { get; set; } = null!;
    }
}
