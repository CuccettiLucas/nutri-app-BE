namespace App_Nutri.Models.Dto
{
    public class CrearCategoriaDto
    {
        public string Nombre { get; set; }
        public int ProfesionalId { get; set; }
        public List<ListaSubCategoriaDto> ListaSubCategoriaDto { get; set; }

    }

    public class ListaSubCategoriaDto
    {
        public string Nombre { get; set; }
        public double PorcionGramos { get; set; }
    }
}
