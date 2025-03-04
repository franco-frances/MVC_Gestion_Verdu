using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services
{
    public class GastosServices:IGastoService
    {

        private readonly VerduGestionDbContext _context;


        public GastosServices(VerduGestionDbContext context)
        {
            _context = context;
            
        }


        public async Task<IEnumerable<Gastos>> GetGastos(int idUsuario)
        {



            var gastos = await _context.Gastos.Where(u=> u.UsuarioId==idUsuario).ToListAsync();
            
            return gastos;


        }

        
        public async Task<Gastos> GetGastoById(int id)
        {

            var gasto = await _context.Gastos.FirstOrDefaultAsync(g => g.IdGasto == id);
            return gasto;


        }
               

        public async Task AgregarGasto(Gastos gasto)
        {

            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
            

        }
                

        public async Task EditarGasto(Gastos gasto)
        {


            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
            

        }


        public async Task EliminarGasto(int id)
        {

            var gasto = await _context.Gastos.FirstOrDefaultAsync(g => g.IdGasto == id);
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
           

        }

    }
}
