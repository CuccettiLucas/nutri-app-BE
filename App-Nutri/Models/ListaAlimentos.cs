namespace App_Nutri.Models
{
    public class ListaAlimentos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ListaAlimentosAlimento> ListaAlimentosAlimentos { get; set; } = new List<ListaAlimentosAlimento>(); // Inicializado vacío por defecto

        public int ProfesionalId { get; set; }
        public string avatar { get; set; }

        public ICollection<AlimentoListaAlimentos> Alimentos { get; set; } = new List<AlimentoListaAlimentos>();
        public ICollection<PacienteListaPersonalizada> PacientesAsignados { get; set; } = new List<PacienteListaPersonalizada>();


    }
}
