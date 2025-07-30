using App_Nutri.Data;
using App_Nutri.Models;
using Microsoft.EntityFrameworkCore;

namespace App_Nutri.Repositories
{
    public interface IUserRepository
    {
        Profesional? GetByEmail(string email);
    }
    public class ProfRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Profesional? GetUserByEmail(string email)
        {
            return _context.Profesionales.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(Profesional usuario)
        {
            _context.Profesionales.Add(usuario);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Profesional>> GetAllUser()
        {
            return  await _context.Profesionales.ToListAsync();        
        }
    }
}
