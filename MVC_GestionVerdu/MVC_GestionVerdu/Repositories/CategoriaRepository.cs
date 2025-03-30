using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly VerduGestionDbContext _context;


        public CategoriaRepository(VerduGestionDbContext context)
        {

            _context = context;

        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {

            var categoria = await _context.Categorias.ToListAsync();
            return categoria;


        }

        public async Task<Categoria> GetbyIdAsync(int id)
        {

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);
            return categoria;


        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();



        }

        public async Task UpdateAsync(Categoria categoria)
        {

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(int id)
        {
            var Categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);
            _context.Categorias.Remove(Categoria);
            await _context.SaveChangesAsync();



        }



    }
}
