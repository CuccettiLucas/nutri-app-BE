namespace App_Nutri.Models
{
    public class HistoriaClinica
    {
        public int id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

        public string Titulo { get; set; }
        public string Descripcion {  get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
