using App_Nutri.Data;
using App_Nutri.Models;
using App_Nutri.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> getCategorias()
        {
            return await _context.Categorias.Include(c => c.SubCategorias).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias
            .Include(c => c.SubCategorias)
            .Include(c => c.Alimentos)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound("La lista no existe.");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Datos de categoría incorrectos.");
            }

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getCategorias), new { id = categoria.Id }, categoria);
        }

        [HttpPost("CrearCateg")]
        public async Task<IActionResult> CrearCateg( CrearCategoriaDto categoriaDto)
        {
            if (string.IsNullOrEmpty(categoriaDto.Nombre))
            {
                return BadRequest("La categoría debe tener un nombre");
            }

            var nuevaCategoria = new Categoria
            {
                Nombre = categoriaDto.Nombre,
                ProfesionalId = categoriaDto.ProfesionalId
                //SubCategorias = await NuevasSubCateg(categoriaDto.ListaSubCategoriaDto)
            };
            _context.Categorias.Add(nuevaCategoria);
            await _context.SaveChangesAsync();

            nuevaCategoria.SubCategorias = await NuevasSubCateg(categoriaDto.ListaSubCategoriaDto, nuevaCategoria.Id , nuevaCategoria.ProfesionalId);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CrearCateg), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        private async Task<List<SubCategoria>> NuevasSubCateg(List<ListaSubCategoriaDto> subcategoriasDto, int categoriaId, int profesionalId)
        {
            if (subcategoriasDto == null || !subcategoriasDto.Any())
            {
                return new List<SubCategoria>();
            }

            var subCategorias = subcategoriasDto.Select(subC => new SubCategoria
            {
                Nombre = subC.Nombre,
                ProfesionalId = profesionalId,
                PorcionGramos = subC.PorcionGramos,
                CategoriaId = categoriaId
            }).ToList();

            _context.SubCategorias.AddRange(subCategorias);
            await _context.SaveChangesAsync();

            return subCategorias;

        }

        [HttpPut("EditarCateg")]
        public async Task<IActionResult> EditarCategoria(EditarCategoriaDto categoriaDto)
        {
            var categoria = await _context.Categorias
                .Include(c => c.SubCategorias)
                .Include(c => c.Alimentos)
                .FirstOrDefaultAsync(c => c.Id == categoriaDto.Id);

            if (categoria == null)
                return NotFound("No se encontro la categoría seleccionada");

            if(!string.IsNullOrEmpty(categoriaDto.Nombre))
                categoria.Nombre = categoriaDto.Nombre;

            if (categoriaDto.SubCategoriasEliminadas != null)
            {
                foreach (var subCategId in categoriaDto.SubCategoriasEliminadas)
                {
                    var subCateg = await _context.SubCategorias.FindAsync(subCategId);
                    if (subCateg != null)
                    {
                        foreach (var alim in subCateg.Alimentos)
                        {
                            alim.SubCategoriaId = null;
                            alim.CategoriaId = categoria.Id;
                        }
                        _context.SubCategorias.Remove(subCateg);
                    }
                }
            }

            foreach (var sc in categoriaDto.ListaSubCategoriaDto)
            {
                var subCateg = await _context.SubCategorias.FirstOrDefaultAsync(s => s.Id == sc.Id & s.CategoriaId == categoria.Id);

                if(subCateg == null)
                {
                    _context.SubCategorias.Add(new SubCategoria
                    {
                        Nombre = !string.IsNullOrEmpty(sc.Nombre) ? sc.Nombre : "Sin nombre",
                        CategoriaId = categoria.Id,
                        ProfesionalId = categoria.ProfesionalId,
                        PorcionGramos = sc.Porcion
                    });
                }
                else
                {
                    subCateg.Nombre = sc.Nombre;
                    subCateg.CategoriaId = categoria.Id;
                    subCateg.ProfesionalId =categoria.ProfesionalId;
                    subCateg.PorcionGramos = sc.Porcion;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(categoria.Id))
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

        [HttpPost("{id}/AgregarSubCategoria")]
        public async Task<ActionResult> AgregarSubCategoria(int id, [FromBody] SubCategoriaDto subCategoriaDto)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if(categoria == null)
            {
                return NotFound(new {Message = "Categoría no encontrada"});
            }

            var subCategoria = new SubCategoria
            {
                Nombre = subCategoriaDto.nombre,
                CategoriaId = subCategoriaDto.categoriaId,
                ProfesionalId = subCategoriaDto.ProfesionalId
            };

            _context.SubCategorias.Add(subCategoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> EditarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //_context.Pacientes.Any(e => e.Id == id);
                var categoriaExist = _context.Categorias.Any(e => e.Id == id);
                if (!categoriaExist)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Eliminar alimentos de la categoría
                var alimentos = _context.Alimentos.Where(a => a.CategoriaId == id);
                _context.Alimentos.RemoveRange(alimentos);

                // Eliminar subcategorías de la categoría
                var subcategorias = _context.SubCategorias.Where(sc => sc.CategoriaId == id);
                _context.SubCategorias.RemoveRange(subcategorias);

                // Eliminar la categoría
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria != null)
                {
                    _context.Categorias.Remove(categoria);
                }

                // Guardar todos los cambios en una sola operación
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error eliminando la categoría: {ex.Message}");
            }
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }

    }
}
