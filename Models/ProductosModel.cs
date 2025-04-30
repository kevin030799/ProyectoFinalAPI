namespace ProyectoFinalAPI.Models
{
    public class ProductosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }

        // Auditoría
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }
    }
}
