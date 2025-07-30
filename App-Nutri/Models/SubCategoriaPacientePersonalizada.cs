namespace App_Nutri.Models
{
    public class SubCategoriaPacientePersonalizada
    {
        public int Id { get; set; }
        public int PacienteListaPersonalizadaId { get; set; }
        public PacienteListaPersonalizada PacienteListaPersonalizada { get; set; }

        public int SubCategoriaId { get; set; }
        public SubCategoria SubCategoria { get; set; }

        public decimal PorcionGramosPersonalizada { get; set; }
    }
}
