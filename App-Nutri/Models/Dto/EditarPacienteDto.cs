using System.Text.Json.Serialization;

namespace App_Nutri.Models.Dto
{
    public class EditarPacienteDto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }

        [JsonPropertyName("correoElectronico")]
        public string? email { get; set; }
        public DateTime nacimiento { get; set; }
        public int listaAlimentos { get; set; }
        public int profAsignado { get; set; }
        public string avatar { get; set; }
        public string role { get; set; }
        public int DNI { get; set; }
        public EditarPorcionDto porcion { get; set; }
        public EditarAnamnesisDto anamnesis {  get; set; }

    }

    public class EditarPorcionDto
    {
        public int carbohidratos { get; set; }
        public int proteinas { get; set; }
        public int fibras { get; set; }
    }

    public class EditarAnamnesisDto
    {
        public int PesoActual { get; set; }
        public int PesoHabitual { get; set; }
        public int Talla { get; set; }
        public List<string> Enfermedades { get; set; }
        public bool TomaMedicacion { get; set; }
        public List<string> Medicaciones { get; set; }
        public List<string> Ejercicio { get; set; }
        public string Trabajo { get; set; }
        public string HorarioLaboral { get; set; }
        public string Cocina { get; set; }
        public int CantidadComidas { get; set; }
        public bool Fumador { get; set; }
        public bool Alcohol { get; set; }
        public List<string> DietasAnteriores { get; set; }
        public string Observaciones { get; set; }
    }
}
