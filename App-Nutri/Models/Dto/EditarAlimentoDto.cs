namespace App_Nutri.Models.Dto
{
    public class EditarAlimentoDto
    {
        public int Id { get; set; }
        public int ProfesionalId { get; set; }
        public string Nombre { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }
        public double Porcion { get; set; }
        public int? CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public List<ListaAlimentosAlimentoDTO> ListaAlimentosAlimentos { get; set; }
    }
}
public class ListaAlimentosAlimentoDTO
{
    public int ListaAlimentosId { get; set; }
}