namespace ProyectoFinalAPI.Models
{
    public class ClientesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        // Dirección
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

        // Auditoría
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? ModificadoPor { get; set; }
    }
}
