using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {


        private readonly VerduGestionDbContext _context;


        public UsuarioRepository(VerduGestionDbContext context)
        {

            _context = context;


        }


        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;


        }

        public async Task<Usuario> GetByIdAsync(int id)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;


        }

        public async Task AddAsync(Usuario usuario)
        {

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();


        }

        public async Task UpdateAsync(Usuario usuario)
        {

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();



        }

        public async Task DeleteAsync(int id)
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();


        }


    }
}
