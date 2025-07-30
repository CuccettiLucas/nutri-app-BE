using App_Nutri.Data;
using App_Nutri.Models;
using App_Nutri.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProfesionalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesional()
        {
            var usuarios = await _context.Profesionales.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/email@gmail.com
        [HttpGet("{email}")]
        public async Task<ActionResult<Profesional>> GetProfData(string email)
        {
            var usuario = await _context.Profesionales.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("prof-by-id/{id}")]
        public async Task<ActionResult<Profesional>> GetProfById(int id)
        {
            var profesional = await _context.Profesionales.FirstOrDefaultAsync(p => p.Id == id);
            if(profesional == null)
            {
                return NotFound("No se encontró el profesional");
            }
            return profesional;
        }

        // PUT
        [HttpPut("update-prof/{id}")]
        public async Task<IActionResult> UpdateProf(int id, UpdateProfDto profesionalDto)
        {
            if (id != profesionalDto.Id)
            {
                return BadRequest("El ID del usuario no coincide.");
            }

            //Obtener Usuario actual de bd
            var usuarioExistente = await _context.Profesionales.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuario No encontrado.");
            }

            // Actualizar campos permitidos
            if (!string.IsNullOrEmpty(profesionalDto.Nombre))
            {
                usuarioExistente.Nombre = profesionalDto.Nombre;
            }

            if (!string.IsNullOrEmpty(profesionalDto.Apellido))
            {
                usuarioExistente.Apellido = profesionalDto.Apellido;
            }

            if (!string.IsNullOrEmpty(profesionalDto.Email)) 
            {
                usuarioExistente.Email = profesionalDto.Email;
            }

            if(profesionalDto.MatriculaMn > 0)
            {
                usuarioExistente.MatriculaMn = profesionalDto.MatriculaMn;
            }

            if (usuarioExistente.MatriculaMp > 0)
            {
                usuarioExistente.MatriculaMp = profesionalDto.MatriculaMp;
            }
            //
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfExists(id)) {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProfExists(int id)
        {
            return _context.Profesionales.Any(e => e.Id == id);
        }
    }
}
