using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IAuthService
    {

        string HashPassword(string password);
        bool VerifyPassword(string inputPassword, string storedHashedPassword);
        Task<Usuario> Register(Register model);
        Task<Usuario> Login(Login model);


    }
}
