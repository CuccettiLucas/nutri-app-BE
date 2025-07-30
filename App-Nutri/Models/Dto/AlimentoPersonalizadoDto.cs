namespace App_Nutri.Models.Dto
{
    public class AlimentoPersonalizadoDto
    {
        public int AlimentoId { get; set; }
        public string Nombre { get; set; }
        public decimal PorcionFinalGramos { get; set; }
        public bool EsPersonalizado { get; set; }
    }
}
