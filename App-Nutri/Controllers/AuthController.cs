using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App_Nutri.Models;
using App_Nutri.Services;
using App_Nutri.Models.Dto;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ProfService _profService;
        private readonly PacienteService _pacienteService;
        private readonly IConfiguration _configuration;

        public AuthController(ProfService profService , PacienteService pacienteService, IConfiguration configuration)
        {
            _profService = profService;
            _pacienteService = pacienteService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Profesional>> GetAllUsers(){

            return await _profService.GetAllUser();

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Llama al servicio de autenticación
            var user = _profService.ValidateCredentials(loginRequest.Email, loginRequest.Password);

            // Verifica si el usuario es válido
            if (user == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = GenerateJwtToken(user.Email, user.Rol);
            return Ok(new { Token = token });
        }

        [HttpPost("LoginPaciente")]
        public IActionResult LoginPaciente([FromBody] LoginPacienteRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Datos de login no proporcionados.");
            }

            // Llama al servicio de autenticación
            var user = _pacienteService.ValidateCredentials(loginRequest.DNI);

            // Verifica si el usuario es válido
            if (user == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = GenerateJwtToken(user.DNI.ToString(), user.Role);
            return Ok(new { 
                Token = token,
                Paciente = new
                {
                    user.Id,
                    user.DNI,
                    user.Role,
                    user.Nombre,
                    user.Apellido,
                    user.CorreoElectronico,
                    user.FechaNacimiento,
                    user.ListaAlimentoId,
                    user.ProfAsignadoId,
                    user.Avatar
                }
            });
        }

        private string GenerateJwtToken(string identifier, string role)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (role == "Paciente")
            {
                claims.Add(new Claim("DNI", identifier));
            }
            else
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identifier)); // Email
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterProfDto model)
        {
            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { message = "Email y contraseña son obligatorios." });

            //Verificar si usuario ya existe
            var existingUser = _profService.GetUsuarioByEmail(model.Email);
            if(existingUser != null)
                return Conflict(new {message = "El usuario ya existe."});

            // Creación de usuario
            _profService.CreateUsuario(model.Email , model.Password, model.Role ?? "Paciente", model.Nombre , model.MatriculaMn , model.matriculaMp);

            return Ok(new {message = "Usuario creado correctamente."});
        }

    }
}
