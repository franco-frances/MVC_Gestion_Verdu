using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using System.Security.Cryptography;
using System.Text;

namespace MVC_GestionVerdu.Services
{
    public class AuthServices:IAuthService
    {

        private readonly VerduGestionDbContext _context;

        public AuthServices(VerduGestionDbContext context)
        {
         
            _context = context;
        }



        public async Task<Usuario> Login(Login model)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null || !VerifyPassword(model.Password, usuario.Password))
            {
                throw new Exception("Email o Contraeña incorrecta");
            }

            return (usuario);


        }






        public async Task<Usuario> Register(Register model)
        {

            // Validar si el correo ya está registrado
            if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email))
            {
                // Lanza una excepción o maneja el error según sea necesario
                throw new Exception("El correo ya está registrado.");
            }

            if (await _context.Usuarios.AnyAsync(u => u.NickName == model.NickName)) {


                throw new Exception("El NickName ya está registrado");
            
            }


            // Crear un nuevo usuario
            var usuario = new Usuario
            {
                Email = model.Email,
                Password = HashPassword(model.Password), // Hashear la contraseña
                NickName = model.NickName // Asignar el nickname
            };

            // Agregar el usuario al contexto y guardar cambios
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario; // Retorna el usuario creado



        }






        public string HashPassword(string password)
        {

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }

        }


       public bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {


            string inputHashed = HashPassword(inputPassword);
            return inputHashed == storedHashedPassword;


        }

        







    }
}
