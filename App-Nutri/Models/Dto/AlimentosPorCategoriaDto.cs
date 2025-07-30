namespace App_Nutri.Models.Dto
{
    public class AlimentosPorCategoriaDto
    {
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public List<ListSubCategoriaDto> Subcategorias { get; set; } = new List<ListSubCategoriaDto>();
    }

    public class ListSubCategoriaDto
    {
        public int? SubcategoriaId { get; set; }
        public string SubcategoriaNombre { get; set;}
        public double Porcion { get; set; }
        public List<AlimentoDto> Alimentos { get; set; } = new List<AlimentoDto>();
    }

    public class AlimentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }
        public double Calorias { get; set; }
        public double Porcion { get; set; }
        public List<ListaAlimentosDto> ListaAlimentos { get; set; }

    }
    public class ListaAlimentosDto
    {
        public int ListaAlimentosId { get; set; }
        public string Nombre { get; set; }
    }
}
