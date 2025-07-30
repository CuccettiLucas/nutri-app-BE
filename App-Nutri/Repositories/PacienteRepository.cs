using App_Nutri.Data;
using App_Nutri.Models;

namespace App_Nutri.Repositories
{
    public class PacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(Paciente usuario)
        {
            _context.Pacientes.Add(usuario);
            _context.SaveChanges();
        }

        public Paciente? GetByDni(int DNI)
        {
            return _context.Pacientes.FirstOrDefault(p => p.DNI == DNI);
        }

    }
}
