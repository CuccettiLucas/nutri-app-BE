namespace App_Nutri.Models.Dto
{
    public class UpdateProfDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido{ get; set; }
        public string? Email { get; set; }
        public int MatriculaMn { get; set; }
        public int MatriculaMp { get; set; }
    }
}
