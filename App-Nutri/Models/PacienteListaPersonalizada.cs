namespace App_Nutri.Models
{
    public class PacienteListaPersonalizada
    {
        public int id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ListaAlimentosId { get; set; }
        public ListaAlimentos ListaAlimentos { get; set; }

        public ICollection<AlimentoPacientePersonalizado> AlimentosPersonalizados { get; set; }
        public ICollection<SubCategoriaPacientePersonalizada> SubCategoriasPersonalizadas { get; set; }
    }
}
