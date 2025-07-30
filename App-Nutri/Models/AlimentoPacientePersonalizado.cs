namespace App_Nutri.Models
{
    public class AlimentoPacientePersonalizado
    {
        public int Id { get; set; }

        public int PacienteListaPersonalizadaId { get; set; }
        public PacienteListaPersonalizada PacienteListaPersonalizada { get; set; }

        public int AlimentoId { get; set; }
        public Alimento Alimento { get; set; }

        public decimal PorcionGramosPersonalizada { get; set; }
    }
}
