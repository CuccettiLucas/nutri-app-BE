namespace App_Nutri.Models
{
    using Newtonsoft.Json;

    public class SubCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double PorcionGramos { get; set; }
        public int ProfesionalId { get; set; }

        // Relación con Categoria principal
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }


        // Relación con Alimentos
        [JsonIgnore]
        public List<Alimento> Alimentos { get; set; } = new List<Alimento>();
    }
}
