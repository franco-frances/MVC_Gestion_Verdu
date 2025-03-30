using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;
using MVC_GestionVerdu.Services.Interfaces;

namespace MVC_GestionVerdu.Services
{
    public class GastosServices:IGastoService
    {

        private readonly IGastosRepository _gastosRepository;


        public GastosServices(IGastosRepository gastosRepository)
        {
            _gastosRepository = gastosRepository;
            
        }
        public async Task<(IEnumerable<Gastos> gastos, int totalRegistros, decimal totalMonto)> GetGastosPaginados(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize)
        {
            return await _gastosRepository.GetGastosPaginadosAsync(usuarioId, fechaInicio, fechaFin, pageNumber, pageSize);
        }





        public async Task<IEnumerable<Gastos>> GetGastos(int idUsuario)
        {


            return await _gastosRepository.GetAllAsync(idUsuario);


        }

        
        public async Task<Gastos> GetGastoById(int id)
        {

            
            return await _gastosRepository.GetByIdAsync(id);


        }

        public async Task<IEnumerable<Gastos>> GetGastosDelDia(int usuarioId, DateTime fechaActual) {


            return await _gastosRepository.GetByDayAysnc(usuarioId, fechaActual);

        }
               

        public async Task AgregarGasto(Gastos gasto)
        {

            await _gastosRepository.AddAsync(gasto);
            

        }
                

        public async Task EditarGasto(Gastos gasto)
        {


            await _gastosRepository.UpdateAsync(gasto);
            

        }


        public async Task EliminarGasto(int id)
        {

            await _gastosRepository.DeleteAsync(id);
           

        }

    }
}
