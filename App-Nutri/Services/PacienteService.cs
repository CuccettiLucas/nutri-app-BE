using App_Nutri.Models;
using App_Nutri.Repositories;

namespace App_Nutri.Services
{
    public class PacienteService
    {
        private readonly PacienteRepository _pacienteRepository;

        public PacienteService( PacienteRepository userRepository)
        {
            _pacienteRepository = userRepository;
        }

        public Paciente? ValidateCredentials(int DNI)
        {
            var paciente = _pacienteRepository.GetByDni(DNI);

            // Si el usuario no existe o la contraseña no coincide, devuelve null
            /*if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;*/

            return paciente;
        }
    }
}
