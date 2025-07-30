namespace App_Nutri.Models
{
    using Newtonsoft.Json;
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProfesionalId { get; set; }
        public List<SubCategoria> SubCategorias { get; set; } = new List<SubCategoria>();
        public List<Alimento> Alimentos { get; set; } = new List<Alimento>();
    }
}
