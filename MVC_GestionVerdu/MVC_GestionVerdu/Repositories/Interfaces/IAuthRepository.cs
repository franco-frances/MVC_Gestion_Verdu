using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        string HashPassword(string password);
        Task<Usuario> Login(Login model);
        Task<Usuario> Register(Register model);
        bool VerifyPassword(string inputPassword, string storedHashedPassword);
    }
}