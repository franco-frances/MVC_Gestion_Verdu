using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services
{
    public class MetodoPagosServices: IMetodoPagoService
    {
        private readonly VerduGestionDbContext _context;


        public MetodoPagosServices(VerduGestionDbContext context)
        {

            _context = context;

        }

        public async Task<IEnumerable<MetodoPago>> GetMetodoPagos()
        {

            var metodoPago = await _context.MetodoPagos.ToListAsync();
            return metodoPago;


        }



    }
}
