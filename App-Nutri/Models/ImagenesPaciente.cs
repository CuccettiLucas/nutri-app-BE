namespace App_Nutri.Models
{
    public class ImagenesPaciente
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Comida { get; set; }
        public string Detalle { get; set; }
        public string UrlImagen { get; set; }
        public string? Observacion { get; set; }
    }
}
