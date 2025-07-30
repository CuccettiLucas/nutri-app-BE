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
    public class ListaAlimentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListaAlimentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaAlimentos>>> GetListasAlimentos()
        {
            return await _context.ListasAlimentos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListaAlimentos>> GetListaAlimento(int id)
        {
            var lista = await _context.ListasAlimentos
                .Include(l => l.ListaAlimentosAlimentos) // Incluir la tabla intermedia
                .ThenInclude(la => la.Alimento) // Incluir los alimentos relacionados
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null)
            {
                return NotFound("La lista no existe.");
            }

            return Ok(lista);
        }

        [HttpGet("ListaByProf/{IdProf}")]
        public IActionResult GetListaByProf(int idProf)
        {
            var listas = _context.ListasAlimentos
                .Include(l => l.ListaAlimentosAlimentos)
                .ThenInclude(la => la.Alimento)
                .Where(lista => lista.ProfesionalId == idProf)
                .ToList();

            if (listas == null || listas.Count == 0)
            {
                return NotFound("No se encontraron listas de alimentos para este profesional");
            }

            return Ok(listas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearListaAlimentos([FromBody] ListaAlimentos listaAlimentos)
        {
            if (listaAlimentos == null)
            {
                return BadRequest("Los datos de la lista son incorrectos.");
            }

            _context.ListasAlimentos.Add(listaAlimentos);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetListaAlimento), new { id = listaAlimentos.Id }, listaAlimentos);
        }

        [HttpPost("{listaId}/agregar-alimentos")]
        public async Task<IActionResult> AgregarAlimentos(int listaId, [FromBody] List<int> alimentosIds)
        {
            var lista = await _context.ListasAlimentos
                .Include(l => l.ListaAlimentosAlimentos) // Incluir la tabla intermedia
                .FirstOrDefaultAsync(l => l.Id == listaId);

            if (lista == null)
            {
                return NotFound("La lista no existe.");
            }

            foreach (var alimentoId in alimentosIds)
            {
                // Evitar duplicados
                if (!lista.ListaAlimentosAlimentos.Any(x => x.AlimentoId == alimentoId))
                {
                    lista.ListaAlimentosAlimentos.Add(new ListaAlimentosAlimento
                    {
                        ListaAlimentosId = listaId,
                        AlimentoId = alimentoId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(lista);
        }

        [HttpPost("AgregarAlimentoALista")]
        public async Task<IActionResult> AgregarAlimentoALista(int listaId, int alimentoId)
        {
            var listaAlimento = new ListaAlimentosAlimento
            {
                ListaAlimentosId = listaId,
                AlimentoId = alimentoId
            };

            _context.Add(listaAlimento);
            await _context.SaveChangesAsync();

            return Ok("Alimento agregado a la lista");
        }

        [HttpPut]
        public async Task<IActionResult> EditarLista(EditarListaAlimentosDto listaAlimentosDto)
        {
            var listaAlimentos = await _context.ListasAlimentos.FindAsync(listaAlimentosDto.Id);

            if(listaAlimentos == null)
            {
                return BadRequest("No se encontró una lista");
            }
            if(listaAlimentosDto.Nombre != null)
            {
                listaAlimentos.Nombre = listaAlimentosDto.Nombre;
            }
            if (listaAlimentosDto.ProfesionalId != 0)
            {
                listaAlimentos.ProfesionalId = listaAlimentosDto.ProfesionalId;
            }
            listaAlimentos.avatar = listaAlimentosDto.avatar;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaExists(listaAlimentos.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetListaAlimento), new { id = listaAlimentos.Id }, listaAlimentos);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> EliminarLista(int id)
        {
            var listaAlim = await _context.ListasAlimentos.FindAsync(id);
            if(listaAlim == null)
            {
                return NotFound();
            }

            _context.ListasAlimentos.Remove(listaAlim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListaExists(int id)
        {
            return _context.ListasAlimentos.Any(e => e.Id == id);
        }


    }
}
