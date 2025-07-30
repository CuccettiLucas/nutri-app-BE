namespace App_Nutri.Models.Dto
{
    public class EditarCategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProfesionalId { get; set; }
        public List<ListaEditarSubCategoriaDto> ListaSubCategoriaDto { get; set; }
        public List<int> SubCategoriasEliminadas { get; set; }

    }

    public class ListaEditarSubCategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Porcion { get; set; }
    }
}
