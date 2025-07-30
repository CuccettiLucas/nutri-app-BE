namespace App_Nutri.Models
{
    public class AlimentoPaciente
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int AlimentoId { get; set; }
        public Alimento Alimento { get; set; }

        public DateTime Fecha { get; set; }
    }
}
