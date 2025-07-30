namespace App_Nutri.Models
{
    public class AlimentoListaAlimentos
    {
        public int Id { get; set; }

        public int ListaAlimentoId { get; set; }
        public ListaAlimentos ListaAlimentos { get; set; }

        public int AlimentoId { get; set; }
        public Alimento Alimento { get; set; }

        public decimal PorcionGramos { get; set; }
    }
}
