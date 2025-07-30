namespace App_Nutri.Models
{
    public class RegistroAlimento
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Profesional Usuario { get; set; } = null!;
        public int AlimentoId { get; set; }
        public Alimento Alimento { get; set; } = null!;
        public DateTime FechaConsumo { get; set; }
        public double Cantidad { get; set; } // En Gramos o Mililitros por ahora 05.12.2024
    }
}
