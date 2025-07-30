using App_Nutri.Data;
using App_Nutri.Migrations;
using App_Nutri.Models;
using App_Nutri.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlimentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Alimentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alimento>>> GetAlimentos()
        {
            return await _context.Alimentos.ToListAsync();
        }

        // GET: api/Alimentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alimento>> GetAlimento(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);

            if (alimento == null)
            {
                return NotFound();
            }

            return alimento;
        }

        // Obtener Alimentos ordenados por categoria por lista de alimentos
        [HttpGet("AlimentosPorListaAlimentos")]
        public async Task<ActionResult<IEnumerable<AlimentosPorCategoriaDto>>> AlimentosPorListaAlimentos(int ProfId, int ListaId, int? pacienteId = null)
        {

            List<AlimentoPacientePersonalizado> alimentosPersonalizados = new();
            List<SubCategoriaPacientePersonalizada> subcategoriasPersonalizadas = new();

            if (pacienteId.HasValue)
            {
                alimentosPersonalizados = await _context.alimentoPacientePersonalizados
                    .Include(ap => ap.PacienteListaPersonalizada)
                    .Include(ap => ap.Alimento)
                    .Where(ap => ap.PacienteListaPersonalizada.PacienteId == pacienteId.Value)
                    .ToListAsync();

                subcategoriasPersonalizadas = await _context.SubCategoriaPacientePersonalizadas
                    .Include(sc => sc.PacienteListaPersonalizada)
                    .Where(sc => sc.PacienteListaPersonalizada.PacienteId == pacienteId.Value)
                    .ToListAsync();
            }

            var alimentos = await _context.Alimentos
                .Where(a => a.ProfesionalId == ProfId && a.ListaAlimentosAlimentos.Any(li => li.ListaAlimentosId == ListaId))
                .Include(a => a.Categoria)
                .Include(a => a.SubCategorias)
                .Include(a => a.ListaAlimentosAlimentos)
                    .ThenInclude(li => li.ListaAlimentos)
                .ToListAsync();

            var alimentosAgrupados = alimentos
                .Where(a => a.Categoria != null)
                .GroupBy(a => new { a.CategoriaId, a.Categoria.Nombre })
                .Select(categoriaGrupo => new AlimentosPorCategoriaDto
                {
                    CategoriaId = categoriaGrupo.Key.CategoriaId.Value,
                    CategoriaNombre = categoriaGrupo.Key.Nombre,
                    Subcategorias = categoriaGrupo
                    .GroupBy(a => new
                    {
                        SubcategoriaId = a.SubCategoriaId,
                        SubcategoriaNombre = a.SubCategorias != null ? a.SubCategorias.Nombre : "Sin subcategoría"
                    })
                    .Select(subcategoriaGrupo =>
                    {
                        var subcategoriaPersonalizada = subcategoriasPersonalizadas
                            .FirstOrDefault(sp => sp.SubCategoriaId == subcategoriaGrupo.Key.SubcategoriaId);

                        double SubcategoriaPorcion = (double?)subcategoriaPersonalizada?.PorcionGramosPersonalizada
                        ?? (double?)(subcategoriaGrupo.FirstOrDefault()?.SubCategorias?.PorcionGramos)
                        ?? 0;

                        return new ListSubCategoriaDto
                        {
                            SubcategoriaId = subcategoriaGrupo.Key.SubcategoriaId,
                            SubcategoriaNombre = !string.IsNullOrEmpty(subcategoriaGrupo.Key.SubcategoriaNombre)
                                ? subcategoriaGrupo.Key.SubcategoriaNombre
                                : "Sin subcategoría",

                            Porcion = SubcategoriaPorcion,
                            Alimentos = subcategoriaGrupo.Select(a =>
                            {
                                var personalizado = alimentosPersonalizados.FirstOrDefault(p => p.AlimentoId == a.Id);

                                return new AlimentoDto
                                {
                                    Id = a.Id,
                                    Nombre = a.Nombre,
                                    Proteinas = a.Proteinas,
                                    Carbohidratos = a.Carbohidratos,
                                    Grasas = a.Grasas,
                                    Calorias = a.Calorias,
                                    Porcion = personalizado?.PorcionGramosPersonalizada != null
                                        ? (double)personalizado.PorcionGramosPersonalizada
                                        : a.Porcion,
                                    ListaAlimentos = a.ListaAlimentosAlimentos
                                        .Where(la => la.ListaAlimentosId == ListaId)
                                        .Select(la => new ListaAlimentosDto
                                        {
                                            ListaAlimentosId = la.ListaAlimentosId,
                                            Nombre = la.ListaAlimentos.Nombre
                                        }).ToList()
                                };
                            }).ToList()
                        };
                    })
                    .OrderBy(s => s.SubcategoriaNombre)
                    .ToList()
                })
                .OrderBy(c => c.CategoriaNombre) // Ordenar categorías alfabéticamente
                .ToList();
            return Ok(alimentosAgrupados);
        }

        /*/ GET: api/Alimentos/AlimentosPorCateg
        [HttpGet("AlimentosPorCateg/{ProfId}")]
        public async Task<ActionResult<IEnumerable<AlimentosPorCategoriaDto>>> AlimentosPorCateg(int ProfId)
        {
            var alimentos = await _context.Alimentos
                .Where(a => a.ProfesionalId == ProfId)
                .Include(a => a.Categoria)
                .Include(a => a.SubCategorias)
                .Include(a => a.ListaAlimentosAlimentos)
                    .ThenInclude(laa => laa.ListaAlimentos)
                .ToListAsync();

            var alimentosAgrupados = alimentos
                .Where(a => a.Categoria != null)
                .GroupBy(a => new { a.CategoriaId, a.Categoria.Nombre })
                .Select(categoriaGrupo => new AlimentosPorCategoriaDto
                {
                    CategoriaId = categoriaGrupo.Key.CategoriaId.Value,
                    CategoriaNombre = categoriaGrupo.Key.Nombre,
                    Subcategorias = categoriaGrupo
                    .GroupBy(a => new
                    {
                        SubcategoriaId = a.SubCategoriaId,
                        SubcategoriaNombre = a.SubCategorias != null ? a.SubCategorias.Nombre : "Sin subcategoría",
                        Porcion = (double)a.SubCategorias.PorcionGramos
                    })
                    .Select(subcategoriaGrupo => new ListSubCategoriaDto
                    {
                        SubcategoriaId = subcategoriaGrupo.Key.SubcategoriaId,
                        SubcategoriaNombre = !string.IsNullOrEmpty(subcategoriaGrupo.Key.SubcategoriaNombre) ? subcategoriaGrupo.Key.SubcategoriaNombre : "Sin subcategoría",
                        Porcion = subcategoriaGrupo.Key.Porcion,
                        Alimentos = subcategoriaGrupo.Select(a => new AlimentoDto
                        {
                            Id = a.Id,
                            Nombre = a.Nombre,
                            Proteinas = a.Proteinas,
                            Carbohidratos = a.Carbohidratos,
                            Grasas = a.Grasas,
                            Calorias = a.Calorias,
                            Porcion = a.Porcion,
                            ListaAlimentos = a.ListaAlimentosAlimentos
                            .Select(la => new ListaAlimentosDto
                            {
                                ListaAlimentosId = la.ListaAlimentosId,
                                Nombre = la.ListaAlimentos.Nombre
                            }).ToList()
                        }).ToList()
                    })
                    .OrderBy(s => s.SubcategoriaNombre)
                    .ToList()
                })
                .OrderBy(c => c.CategoriaNombre) // Ordenar categorías alfabéticamente
                .ToList();
            return Ok(alimentosAgrupados);
        }*/

        [HttpGet("AlimentosPorCateg/{ProfId}")]
        public async Task<ActionResult<IEnumerable<AlimentosPorCategoriaDto>>> AlimentosPorCateg(int ProfId)
        {
            var categorias = await _context.Categorias
                .Where(c => c.ProfesionalId == ProfId)
                .Include(c => c.SubCategorias)
                .ToListAsync();

            var alimentos = await _context.Alimentos
                .Where(a => a.ProfesionalId == ProfId)
                .Include(a => a.ListaAlimentosAlimentos)
                    .ThenInclude(laa => laa.ListaAlimentos)
                .ToListAsync();

            var alimentosAgrupados = categorias
                .Select(c => {
                    var subcategorias = new List<ListSubCategoriaDto>();

                    if (c.SubCategorias != null && c.SubCategorias.Any())
                    {
                        // Agregar subcategorías reales
                        subcategorias.AddRange(c.SubCategorias.Select(sc =>
                        {
                            var alimentosEnSub = alimentos
                                .Where(a => a.SubCategoriaId == sc.Id)
                                .ToList();

                            return new ListSubCategoriaDto
                            {
                                SubcategoriaId = sc.Id,
                                SubcategoriaNombre = sc.Nombre,
                                Porcion = sc.PorcionGramos,
                                Alimentos = alimentosEnSub.Select(a => new AlimentoDto
                                {
                                    Id = a.Id,
                                    Nombre = a.Nombre,
                                    Proteinas = a.Proteinas,
                                    Carbohidratos = a.Carbohidratos,
                                    Grasas = a.Grasas,
                                    Calorias = a.Calorias,
                                    Porcion = a.Porcion,
                                    ListaAlimentos = a.ListaAlimentosAlimentos.Select(la => new ListaAlimentosDto
                                    {
                                        ListaAlimentosId = la.ListaAlimentosId,
                                        Nombre = la.ListaAlimentos.Nombre
                                    }).ToList()
                                }).ToList()
                            };
                        }));
                    }

                    // Alimentos sin subcategoría
                    var alimentosSinSub = alimentos
                        .Where(a => a.CategoriaId == c.Id && a.SubCategoriaId == null)
                        .ToList();

                    if (alimentosSinSub.Any() || !subcategorias.Any())
                    {
                        subcategorias.Add(new ListSubCategoriaDto
                        {
                            SubcategoriaId = 0,
                            SubcategoriaNombre = "Sin subcategoría",
                            Porcion = 0,
                            Alimentos = alimentosSinSub.Select(a => new AlimentoDto
                            {
                                Id = a.Id,
                                Nombre = a.Nombre,
                                Proteinas = a.Proteinas,
                                Carbohidratos = a.Carbohidratos,
                                Grasas = a.Grasas,
                                Calorias = a.Calorias,
                                Porcion = a.Porcion,
                                ListaAlimentos = a.ListaAlimentosAlimentos.Select(la => new ListaAlimentosDto
                                {
                                    ListaAlimentosId = la.ListaAlimentosId,
                                    Nombre = la.ListaAlimentos.Nombre
                                }).ToList()
                            }).ToList()
                        });
                    }

                    return new AlimentosPorCategoriaDto
                    {
                        CategoriaId = c.Id,
                        CategoriaNombre = c.Nombre,
                        Subcategorias = subcategorias.OrderBy(s => s.SubcategoriaNombre).ToList()
                    };
                })
                .OrderBy(c => c.CategoriaNombre)
                .ToList();

            return Ok(alimentosAgrupados);
        }



        // POST: api/Alimentos
        [HttpPost]
        public async Task<ActionResult<Alimento>> PostAlimento(CrearAlimentoDto alimentoDto)
        {
            if (string.IsNullOrEmpty(alimentoDto.nombre))
            {
                return BadRequest("Necesitamos un nombre para crear el alimento.");
            }

            if (alimentoDto.porcion == 0)
            {
                return BadRequest("Completar los datos nutricionales del alimento.");
            }

            if (alimentoDto.CategoriaId == 0)
            {
                return BadRequest("Debe seleccionar una categoría para el alimento.");
            }

            var alimento = new Alimento
            {
                ProfesionalId = alimentoDto.profesionalId,
                Nombre = alimentoDto.nombre,
                Proteinas = alimentoDto.proteinas,
                Carbohidratos = alimentoDto.carbohidratos,
                Grasas = alimentoDto.grasas,
                Calorias = CalcularCalorias(alimentoDto.carbohidratos, alimentoDto.proteinas, alimentoDto.grasas),
                Porcion = alimentoDto.porcion,
                CategoriaId = alimentoDto.CategoriaId,
                SubCategoriaId = alimentoDto.SubCategoriaId > 0 ? alimentoDto.SubCategoriaId : null,
                SubCategorias = null
            };

            _context.Alimentos.Add(alimento);
            await _context.SaveChangesAsync();

            //Asignar Alimento a Listas de Alimentos
            if (alimentoDto.ListaAlimentosIds != null && alimentoDto.ListaAlimentosIds.Any())
            {
                var relaciones = alimentoDto.ListaAlimentosIds.Select(listaId => new ListaAlimentosAlimento
                {
                    ListaAlimentosId = listaId,
                    AlimentoId = alimento.Id
                });

                _context.ListaAlimentosAlimentos.AddRange(relaciones);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetAlimento), new { id = alimento.Id }, alimento);
        }

        private double CalcularCalorias(double carbohidratos, double proteinas, double grasas)
        {
            return (carbohidratos * 4) + (proteinas * 4) + (grasas * 9);
        }

        [HttpPut]
        public async Task<ActionResult<Alimento>> EditarAlimento(EditarAlimentoDto alimentoDto)
        {
            var alimento = await _context.Alimentos
                .Include(a => a.Categoria)
                .Include(a => a.SubCategorias)
                .Include(a => a.ListaAlimentosAlimentos)
                .FirstOrDefaultAsync(a => a.Id == alimentoDto.Id);

            if (alimento == null)
            {
                return BadRequest("Alimento no encontrado");
            }

            alimento.ProfesionalId = alimentoDto.ProfesionalId;

            if (!string.IsNullOrEmpty(alimentoDto.Nombre))
            {
                alimento.Nombre = alimentoDto.Nombre;
            }

            alimento.Proteinas = alimentoDto.Proteinas;
            alimento.Carbohidratos = alimentoDto.Carbohidratos;
            alimento.Grasas = alimentoDto.Grasas;
            alimento.Calorias = CalcularCalorias(alimentoDto.Carbohidratos, alimentoDto.Proteinas, alimentoDto.Grasas);

            if (alimentoDto.Porcion <= 0)
            {
                return BadRequest("La porcion del alimento debe ser mayor a 0");
            }

            /* RELACION ALIMENTO - Categoria/SubCateg */
            if (alimentoDto.CategoriaId <= 0)
            {
                return BadRequest("Seleccionar una categoría");
            }
            else
            {
                alimento.CategoriaId = alimentoDto.CategoriaId;
                alimento.Categoria = await _context.Categorias.FindAsync(alimentoDto.CategoriaId);
            }

            if (alimentoDto.SubCategoriaId > 0)
            {
                alimento.SubCategorias = await _context.SubCategorias.FindAsync(alimentoDto.SubCategoriaId);
            }

            /* RELACION Alimento - ListaAlimentos */
            //Obtener Lista de alimentos que se encuentre el alimento
            var listasAlimentosBd = await _context.ListaAlimentosAlimentos
                .Where(li => li.AlimentoId == alimentoDto.Id)
                .ToListAsync();

            // Agregar nueva relacion entre ListaAlimentos y Alimentos si no existe tal relación
            foreach (var nuevaRelacion in alimentoDto.ListaAlimentosAlimentos)
            {
                var listaAlimento = listasAlimentosBd
                    .FirstOrDefault(la => la.ListaAlimentosId == nuevaRelacion.ListaAlimentosId);

                if (listaAlimento == null)
                {
                    _context.ListaAlimentosAlimentos.Add(new ListaAlimentosAlimento
                    {
                        ListaAlimentosId = nuevaRelacion.ListaAlimentosId,
                        AlimentoId = alimentoDto.Id // Asegúrate de incluir el ID del alimento
                    });

                }
            }

            // Se actualiza la relacion entre ListaAlimentos y Alimentos Si ya existe la realción
            foreach (var actualizarRelacion in listasAlimentosBd)
            {
                var actualizacion = alimentoDto.ListaAlimentosAlimentos
                    .FirstOrDefault(la => la.ListaAlimentosId == actualizarRelacion.ListaAlimentosId);

                if (actualizacion == null) // Si no está en el DTO, eliminar de la BD
                {
                    _context.ListaAlimentosAlimentos.Remove(actualizarRelacion);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentoExists(alimento.Id))
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


        // DELETE: api/Alimento/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> DeleteAlimento(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento == null)
            {
                return NotFound();
            }

            _context.Alimentos.Remove(alimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlimentoExists(int id)
        {
            return _context.Alimentos.Any(e => e.Id == id);
        }


    }
}
