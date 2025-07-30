using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_Nutri.Data;
using App_Nutri.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using App_Nutri.Models.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App_Nutri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PacientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        [HttpGet("IdProfesional/{idProf}")]
        [Authorize(Roles = "Profesional")]
        public async Task<ActionResult<Paciente>> GetPacientesToProf(int idProf)
        {
            var pacientes = await _context.Pacientes.Where(p => p.ProfAsignadoId == idProf).ToListAsync();

            if (!pacientes.Any())
            {
                return NotFound();
            }

            return Ok(pacientes);
        }

        [HttpGet("DNI/{DNI}")]
        public async Task<ActionResult> ValidDNI(int DNI)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.DNI == DNI);

            if (paciente == null)
            {
                return NotFound(new { mensaje = "Paciente no encontrado" });
            }

            var token = GenerateJwtToken(DNI.ToString());
            return Ok(new { Token = token });
        }

        [HttpGet("GetPacienteData/{DNI}")]
        public async Task<ActionResult<Paciente>> GetPacienteData( int DNI)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.DNI == DNI);

            if(paciente == null)
            {
                return NotFound(new {mensaje = "Paciente no detectado"});
            }

            return Ok(paciente);
        }

        private string GenerateJwtToken(string DNI)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, DNI),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        //[Authorize(Roles = "Profesional")]
        public async Task<ActionResult<NuevoPacienteDto>> PostPaciente(NuevoPacienteDto pacienteDto)
        {

            if (string.IsNullOrEmpty(pacienteDto.nombre))
            {
                return BadRequest("El nombre y apellido son obligatorios.");
            }
            var ExistePaciente = PacienteExistsDNI(pacienteDto.DNI);
            if (ExistePaciente)
            {
                return BadRequest("Ya existe otro paciente con el mismo DNI");

            }

            var paciente = new Paciente
            {

                Nombre = pacienteDto.nombre,
                Apellido = pacienteDto.apellido,
                CorreoElectronico = pacienteDto.email,
                FechaNacimiento = pacienteDto.nacimiento,
                ListaAlimentoId = pacienteDto.listaAlimentos,
                ProfAsignadoId = pacienteDto.profAsignado,
                Avatar = pacienteDto.avatar,
                DNI = pacienteDto.DNI,
                Role = pacienteDto.role,
                // Datos de Procion
                Porcion = new Porcion
                {
                    Carbohidratos = pacienteDto.porcion.carbohidratos,
                    Proteinas = pacienteDto.porcion.proteinas,
                    Fibras = pacienteDto.porcion.fibras
                },
                // Datos de Anamnesis
                Anamnesis = new Anamnesis
                {
                    PesoActual = pacienteDto.anamnesis.PesoActual,
                    PesoHabitual = pacienteDto.anamnesis.PesoHabitual,
                    Talla = pacienteDto.anamnesis.Talla,
                    Enfermedades = pacienteDto.anamnesis.Enfermedades,
                    TomaMedicacion = pacienteDto.anamnesis.TomaMedicacion,
                    Medicaciones = pacienteDto.anamnesis.Medicaciones,
                    Ejercicio = pacienteDto.anamnesis.Ejercicio,
                    Trabajo = pacienteDto.anamnesis.Trabajo,
                    HorarioLaboral = pacienteDto.anamnesis.HorarioLaboral,
                    Cocina = pacienteDto.anamnesis.Cocina,
                    CantidadComidas = pacienteDto.anamnesis.CantidadComidas,
                    Fumador = pacienteDto.anamnesis.Fumador,
                    Alcohol = pacienteDto.anamnesis.Alcohol,
                    DietasAnteriores = pacienteDto.anamnesis.DietasAnteriores,
                    Observaciones = pacienteDto.anamnesis.Observaciones
                }
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            // Crear Viculo Paciente ListaPersonalizada
            var listaPersonalizada = new PacienteListaPersonalizada
            {
                PacienteId = paciente.Id,
                ListaAlimentosId = pacienteDto.listaAlimentos
            };
            _context.PacienteListaPersonalizadas.Add(listaPersonalizada);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        [HttpPut("EditarPaciente")]
        public async Task<IActionResult> EditarPaciente(EditarPacienteDto pacienteDto)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteDto.id);

            if(paciente == null)
            {
                return NotFound("Paciente no encontrado");
            }

            if (!string.IsNullOrEmpty(pacienteDto.nombre))
            {
                paciente.Nombre = pacienteDto.nombre;
            }

            if (!string.IsNullOrEmpty(pacienteDto.apellido))
            {
                paciente.Apellido = pacienteDto.apellido;
            }

            if (!string.IsNullOrEmpty(pacienteDto.email))
            {
                paciente.CorreoElectronico = pacienteDto.email;
            }

            if (pacienteDto.DNI > 0)
            {
                paciente.DNI = pacienteDto.DNI;
            }

            paciente.FechaNacimiento = pacienteDto.nacimiento;
            paciente.ListaAlimentoId = pacienteDto.listaAlimentos;
            paciente.ProfAsignadoId = pacienteDto.profAsignado;
            paciente.Avatar = pacienteDto.avatar;
            paciente.Role = pacienteDto.role;
            //Datos de Porcion
            paciente.Porcion.Carbohidratos = pacienteDto.porcion.carbohidratos;
            paciente.Porcion.Proteinas = pacienteDto.porcion.proteinas;
            paciente.Porcion.Fibras = pacienteDto.porcion.fibras;
            // Datos de Anamnesis
            paciente.Anamnesis.PesoActual = pacienteDto.anamnesis.PesoActual;
            paciente.Anamnesis.PesoHabitual = pacienteDto.anamnesis.PesoHabitual;
            paciente.Anamnesis.Talla = pacienteDto.anamnesis.Talla;
            paciente.Anamnesis.Enfermedades = pacienteDto.anamnesis.Enfermedades;
            paciente.Anamnesis.TomaMedicacion = pacienteDto.anamnesis.TomaMedicacion;
            paciente.Anamnesis.Medicaciones = pacienteDto.anamnesis.Medicaciones;
            paciente.Anamnesis.Ejercicio = pacienteDto.anamnesis.Ejercicio;
            paciente.Anamnesis.Trabajo = pacienteDto.anamnesis.Trabajo;
            paciente.Anamnesis.HorarioLaboral = pacienteDto.anamnesis.HorarioLaboral;
            paciente.Anamnesis.Cocina = pacienteDto.anamnesis.Cocina;
            paciente.Anamnesis.CantidadComidas = pacienteDto.anamnesis.CantidadComidas;
            paciente.Anamnesis.Fumador = pacienteDto.anamnesis.Fumador;
            paciente.Anamnesis.Alcohol = pacienteDto.anamnesis.Alcohol;
            paciente.Anamnesis.DietasAnteriores = pacienteDto.anamnesis.DietasAnteriores;
            paciente.Anamnesis.Observaciones = pacienteDto.anamnesis.Observaciones;
            //
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(paciente.Id))
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
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }

        private bool PacienteExistsDNI(int dni)
        {
            return _context.Pacientes.Any(e => e.DNI == dni);
        }

        /*Método para modificacion de porcion personalizada*/
        [HttpPut("{pacienteId}/personalizar-porcion")]
        public async Task<IActionResult> PersonalizarPorcion(int pacienteId, [FromBody] PorcionPersonalizadaDto dto)
        {
            var listaPersonalizada = await _context.PacienteListaPersonalizadas
                .FirstOrDefaultAsync(x => x.PacienteId == pacienteId);

            if (listaPersonalizada == null)
            {
                return NotFound("El paciente no tiene una lista personalizada.");
            }

            var existente = await _context.alimentoPacientePersonalizados
                .FirstOrDefaultAsync(x=>
                    x.PacienteListaPersonalizadaId == listaPersonalizada.id &&
                    x.AlimentoId == dto.AlimentoId);

            if (existente != null)
            {
                existente.PorcionGramosPersonalizada = dto.PorcionGramos;
                _context.Update(existente);
            }
            else
            {
                var nuevo = new AlimentoPacientePersonalizado
                {
                    PacienteListaPersonalizadaId = listaPersonalizada.id,
                    AlimentoId = dto.AlimentoId,
                    PorcionGramosPersonalizada = dto.PorcionGramos
                };
                _context.alimentoPacientePersonalizados.Add(nuevo);
            }

            await _context.SaveChangesAsync();
            return Ok("Porcion personalizada actualizada.");
        }

        [HttpGet("{pacienteId}/lista-personalizada")]
        public async Task<ActionResult<IEnumerable<AlimentoPersonalizadoDto>>> GetListaPersonalizada (int pacienteId)
        {
            var listaPersonalizada = await _context.PacienteListaPersonalizadas
                .Include(plp => plp.AlimentosPersonalizados)
                .FirstOrDefaultAsync(plp => plp.PacienteId == pacienteId);

            if (listaPersonalizada == null)
            {
                return NotFound("El paciente no tiene una lista personalizada asignada");
            }

            var listaBase = await _context.ListaAlimentosAlimentos
                .Where(ala => ala.ListaAlimentosId == listaPersonalizada.ListaAlimentosId)
                .Include(ala => ala.Alimento)
                .ToListAsync();


            var alimentosFinales = listaBase.Select(ala =>
            {
                var personalizado = listaPersonalizada.AlimentosPersonalizados
                .FirstOrDefault(ap => ap.AlimentoId == ala.AlimentoId);

                return new AlimentoPersonalizadoDto
                {
                    AlimentoId = ala.AlimentoId,
                    Nombre = ala.Alimento.Nombre,
                    PorcionFinalGramos = personalizado?.PorcionGramosPersonalizada ?? ala.PorcionGramos,
                    EsPersonalizado = personalizado != null
                };
            }).ToList();

            return Ok(alimentosFinales);
        }
    }
}
