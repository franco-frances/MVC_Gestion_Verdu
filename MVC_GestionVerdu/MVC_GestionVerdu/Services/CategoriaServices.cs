using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;



namespace MVC_GestionVerdu.Services
{

  



    public class CategoriaServices:ICategoriaService
    {

        private readonly VerduGestionDbContext _context;


        public CategoriaServices(VerduGestionDbContext context)
        {
            
            _context = context;

        }




        public async Task<IEnumerable<Categoria>> GetCategorias()
        {

            var categoria = await _context.Categorias.ToListAsync();
            return categoria;


        }

        

        public async Task<Categoria> GetCategoriabyId(int id)
        {

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);
            return categoria;


        }



        public async Task AgregarCategoria( Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            


        }


        public async Task EditarCategoria(Categoria categoria)
        {

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
           

        }



       

        public async Task EliminarCategoria(int id)
        {
            var Categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);
            _context.Categorias.Remove(Categoria);
            await _context.SaveChangesAsync();
           


        }









    }
}
