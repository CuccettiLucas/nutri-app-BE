namespace App_Nutri.Models
{
    public class Anamnesis
    {
        public int PesoActual {  get; set; } 
        public int PesoHabitual { get; set; }
        public int Talla { get; set; }
        public List<string> Enfermedades { get; set; }
        public bool TomaMedicacion {  get; set; }
        public List<string> Medicaciones { get; set; }
        public List<string> Ejercicio { get; set; }
        public string Trabajo { get; set; }
        public string HorarioLaboral { get; set; }
        public string Cocina { get; set; }
        public int CantidadComidas { get; set; }
        public bool Fumador {  get; set; }
        public bool Alcohol { get; set; }
        public List<string> DietasAnteriores { get; set; }
        public string Observaciones { get; set; }
    }
}
