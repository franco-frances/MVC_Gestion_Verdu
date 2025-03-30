using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class MetodoPagosRepository : IMetodoPagosRepository
    {

        private readonly VerduGestionDbContext _context;


        public MetodoPagosRepository(VerduGestionDbContext context)
        {

            _context = context;

        }


        public async Task<IEnumerable<MetodoPago>> GetAllasync()
        {

            var metodoPago = await _context.MetodoPagos.ToListAsync();
            return metodoPago;


        }


        public async Task<MetodoPago> GetByIdAsync(int id)
        {

            var MetodoPago = await _context.MetodoPagos.FirstOrDefaultAsync(m => m.IdMetodoPago == id);
            return MetodoPago;


        }


        public async Task AddAsync(MetodoPago metodoPago)
        {
            await _context.MetodoPagos.AddAsync(metodoPago);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateAsync(MetodoPago metodoPago)
        {

            _context.MetodoPagos.Update(metodoPago);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(int id)
        {

            var metodoPago = await _context.MetodoPagos.FirstOrDefaultAsync(m => m.IdMetodoPago == id);
            _context.MetodoPagos.Remove(metodoPago);
            await _context.SaveChangesAsync();


        }







    }
}
