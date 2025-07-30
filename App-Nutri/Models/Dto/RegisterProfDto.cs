namespace App_Nutri.Models.Dto
{
    public class RegisterProfDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public int MatriculaMn {  get; set; }
        public int matriculaMp { get; set; }
    }
}
