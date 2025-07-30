namespace App_Nutri.Models.Dto
{
    public class EditarHistoriaClinicaDto
    {
        public int id { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
