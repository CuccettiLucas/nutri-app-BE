namespace App_Nutri.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public int DNI { get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? ListaAlimentoId { get; set; }
        public int? ProfAsignadoId { get; set; }
        public string Avatar { get; set; }
        public Porcion Porcion { get; set; }
        public List<HistoriaClinica>? HistoriaClinica { get; set; }
        public  Anamnesis? Anamnesis { get; set; }

        public ICollection<PacienteListaPersonalizada> ListasPersonalizadas { get; set; }
    }
}
