using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace App_Nutri.Models
{
    [Table("Alimentos")]
    public class Alimento
    {
        public int Id { get; set; }
        public int ProfesionalId { get; set; }
        public string Nombre { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }
        public double Calorias { get; set; }
        public double Porcion { get; set; }
        public double CantidadEquivalencia { get; set; }
        public string UnidadEquivalencia { get; set; }

        //Relacion directa con Categoria
        public int? CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria Categoria { get; set; }

        //Relacion Opcional con Subcatecoria
        public int? SubCategoriaId { get; set; }
        [JsonIgnore]
        public SubCategoria SubCategorias { get; set; } = new SubCategoria();
        public List<ListaAlimentosAlimento> ListaAlimentosAlimentos { get; set; } = new List<ListaAlimentosAlimento>(); // Inicializado vacío por defecto

    }
}
