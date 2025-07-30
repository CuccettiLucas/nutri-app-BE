using System.Security.Cryptography;
using System.Text;
using App_Nutri.Models;
using App_Nutri.Repositories;

namespace App_Nutri.Services
{
    public interface IAuthService
    {
        Profesional? ValidateCredentials(string email, string password);
    }
    public class ProfService
    {
        private readonly ProfRepository _profRepository;

        public ProfService(ProfRepository userRepository)
        {
            _profRepository = userRepository;
        }

        public Profesional? ValidateCredentials(string email, string password)
        {
            var user = _profRepository.GetUserByEmail(email);

            // Si el usuario no existe o la contraseña no coincide, devuelve null
            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(computedHash) == storedHash;
            }
        }

        public Profesional GetUsuarioByEmail(string email) {
            return _profRepository.GetUserByEmail(email);
        }

        public Task<IEnumerable<Profesional>> GetAllUser()
        {
            return _profRepository.GetAllUser();
        }

        public void CreateUsuario(string email, string password, string role, string nombre, int matriculaMn, int matriculaMp)
        {
            // Genera el hash y el salt de la contraseña
            var (hash, salt) = CreatePasswordHash(password);

            var usuario = new Profesional
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Rol =role,
                Nombre = nombre,
                MatriculaMn = matriculaMn,
                MatriculaMp = matriculaMp
            };

            _profRepository.AddUser(usuario);
        }

        private (string Hash, string Salt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = Convert.ToBase64String(hmac.Key); // Clave única para este usuario
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (hash, salt);
            }
        }

    }
}
