using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using App_Nutri.Services;
using App_Nutri.Data;
using App_Nutri.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/img")]
public class ImagenController : ControllerBase
{
    private readonly IStorageService _storageService;
    private readonly ApplicationDbContext _context;

    public ImagenController(IStorageService storageService, ApplicationDbContext context)
    {
        _storageService = storageService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetImgForMonth(DateTime start , DateTime end, int pacienteId)
    {
        if (start == default(DateTime) || end == default(DateTime))
        {
            return BadRequest("No se encontró rango de fechas");
        }
        var rangeDateTime = _context.ImagenesPacientes
            .Where(r => r.FechaCarga >= start && r.FechaCarga <= end && r.PacienteId == pacienteId)
            .ToList();

        return Ok(rangeDateTime);
    }

    [HttpPost("subir")]
    public async Task<IActionResult> SubirImagen([FromForm] IFormFile archivo, [FromForm] int pacienteId, [FromForm] string comida, [FromForm] string detalle, [FromForm] DateTime fechaCarga)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("Debes subir una imagen válida.");

        if(string.IsNullOrEmpty(comida))
            return BadRequest("Debe inidcar que tipo de comida.");

        var url = await _storageService.SubirImg(archivo);

        var imagenPaciente = new ImagenesPaciente
        {
            PacienteId = pacienteId,
            Comida = comida,
            Detalle = detalle,
            FechaCarga = fechaCarga,
            UrlImagen = url
        };

        _context.Add(imagenPaciente);
        await _context.SaveChangesAsync();

        return Ok(new { imagenPaciente });
    }

    [HttpPut("Observacion")]
    public async Task<IActionResult> ObservacionProf([FromForm] string obs, [FromForm] int idImg)
    {
        if (string.IsNullOrEmpty(obs))
            return BadRequest("Debe completar el campo Observación");

        var img = await _context.ImagenesPacientes.FirstOrDefaultAsync(i => i.Id == idImg);

        if (img == null)
            return NotFound("No se encontro la imagen");

        img.Observacion = obs;
        _context.Update(img);
        await _context.SaveChangesAsync();
        return Ok(new { img.Observacion });

    }

    [HttpDelete("eliminar")]
    public async Task<IActionResult> EliminarImagen([FromQuery] string url , [FromQuery] int id)
    {
        var resultado = await _storageService.EliminarImagenAsync(url);
        if (!resultado)
            return BadRequest("No se pudo eliminar la imagen.");

        var ImgDelete = await _context.ImagenesPacientes.FindAsync(id);
        if (ImgDelete == null)
        {
            return NotFound();
        }
        _context.Remove(ImgDelete);
        _context.SaveChanges();

        return Ok("Imagen eliminada correctamente.");
    }
}
