using App_Nutri.Data;
using App_Nutri.Models;
using App_Nutri.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriaClinicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoriaClinicaController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{PacienteId}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinica(int PacienteId) {

            var historia = await _context.HistoriaClinica.Where(h => h.PacienteId == PacienteId).ToListAsync();

            if (!historia.Any())
            {
                return NotFound();
            }

            return Ok(historia);

        }

        [HttpPost]
        public async Task<ActionResult<CrearHistoriaClinicaDto>> CrearHistoriaClinica(CrearHistoriaClinicaDto historiaClinicaDto) {

            if (historiaClinicaDto.PacienteId <= 0)
                return BadRequest("El id del paciente es incorrecto");

            if (historiaClinicaDto.ProfesionalId <= 0)
                return BadRequest("El id del profesional es incorrecto");

            if (string.IsNullOrEmpty(historiaClinicaDto.Titulo))
                return BadRequest("Debe llevar un título");

            var historia = new HistoriaClinica
            {
                PacienteId = historiaClinicaDto.PacienteId,
                ProfesionalId = historiaClinicaDto.ProfesionalId,
                Titulo = historiaClinicaDto.Titulo,
                Descripcion = historiaClinicaDto.Descripcion,
                FechaCreacion = historiaClinicaDto.FechaCreacion
            };

            _context.Add(historia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHistoriaClinica), new { PacienteId = historia.PacienteId}, historia);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarHistoriaClinica(EditarHistoriaClinicaDto historiaClinicaDto)
        {
            if (historiaClinicaDto.id < 0)
                return BadRequest("El id del Historial item es incorrecto");

            if (historiaClinicaDto.PacienteId <= 0)
                return BadRequest("El id del paciente es incorrecto");

            if (historiaClinicaDto.ProfesionalId <= 0)
                return BadRequest("El id del profesional es incorrecto");

            if (string.IsNullOrEmpty(historiaClinicaDto.Titulo))
                return BadRequest("Debe llevar un título");

            var historia = await _context.HistoriaClinica.FindAsync(historiaClinicaDto.id);

            if (historia == null)
                return BadRequest("No se encontró item de historia clinica");

            historia.PacienteId = historiaClinicaDto.PacienteId;
            historia.ProfesionalId = historiaClinicaDto.ProfesionalId;
            historia.Titulo = historiaClinicaDto.Titulo;
            historia.Descripcion = historiaClinicaDto.Descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoriaExists(historia.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        private bool HistoriaExists(int id)
        {
            return _context.HistoriaClinica.Any(e => e.id == id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> EliminarItemHistoria(int id)
        {
            var historia = await _context.HistoriaClinica.FirstOrDefaultAsync(e => e.id == id);

            if (historia == null) { return NotFound(); }

            _context.HistoriaClinica.Remove(historia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
