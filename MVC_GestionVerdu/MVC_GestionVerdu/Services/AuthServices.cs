using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;
using MVC_GestionVerdu.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MVC_GestionVerdu.Services
{
    public class AuthServices:IAuthService
    {

        private readonly IAuthRepository _authRepository;

        public AuthServices(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }



        public async Task<Usuario> Login(Login model)
        {
            // Delegamos la operación de login al repositorio.
            return await _authRepository.Login(model);
        }



        public async Task<Usuario> Register(Register model)
        {
            // Delegamos la operación de registro al repositorio.
            return await _authRepository.Register(model);
        }









        

        







    }
}
