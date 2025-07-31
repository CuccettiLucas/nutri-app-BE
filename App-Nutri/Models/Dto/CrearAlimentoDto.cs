namespace App_Nutri.Models.Dto
{
    public class CrearAlimentoDto
    {
        public int profesionalId { get; set; }
        public string nombre { get; set; }
        public double proteinas { get; set; }
        public double carbohidratos { get; set; }
        public double grasas {  get; set; }
        public double porcion {  get; set; }
        public int CategoriaId { get; set; }
        public int? SubCategoriaId { get; set; }
        public List<int> ListaAlimentosIds { get; set; } = new List<int>();
        public double cantidadEquivalencia { get; set; }
        public string unidadEquivalencia {  get; set; }
    }
}
