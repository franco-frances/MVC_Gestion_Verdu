using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services.Interfaces
{
    public interface IAuthService
    {

        
        Task<Usuario> Register(Register model);
        Task<Usuario> Login(Login model);


    }
}
