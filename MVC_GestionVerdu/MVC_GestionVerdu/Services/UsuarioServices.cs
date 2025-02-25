using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;


namespace MVC_GestionVerdu.Services
{
        

    public class UsuarioServices:IUsuarioService
    {
        private readonly VerduGestionDbContext _context;

        public UsuarioServices(VerduGestionDbContext context)
        {
            
            _context = context;
        }




        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;


        }



        public async Task<Usuario> GetUsuariosById(int id)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;


        }


    
        public async Task AgregarUsuario(Usuario usuario)
        {

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

           
        }



        

        public async Task EditarUsuario(Usuario usuario)
        {

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

           

        }

      

        public async Task EliminarUsuario(int id)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

           
        }




    }
}
