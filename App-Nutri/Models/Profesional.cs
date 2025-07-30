namespace App_Nutri.Models
{
    public class Profesional
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Paciente";
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int MatriculaMn { get; set; }
        public int MatriculaMp { get; set; }
        public List<HistoriaClinica>? HistoriaClinica { get; set; }
    }
}
