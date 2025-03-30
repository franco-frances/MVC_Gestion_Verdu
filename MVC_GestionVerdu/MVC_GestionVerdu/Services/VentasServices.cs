using Microsoft.EntityFrameworkCore.ChangeTracking;
using MVC_GestionVerdu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Services
{
    public class VentasServices:IVentaService
    {

        private readonly IVentasRepository _ventasRepository;

        public VentasServices(IVentasRepository ventasRepository)
        {

            _ventasRepository = ventasRepository;
            
        }

        public async Task<IEnumerable<DetallesVenta>> GetVentas(int usuarioId)
        {
            

            return await _ventasRepository.GetAllAsync(usuarioId);
        }



        public async Task<(IEnumerable<DetallesVenta> ventas, int totalRegistros, decimal totalMonto)> GetVentasPaginadas(int usuarioId, string? metodoPago, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize) {

            return await _ventasRepository.GetVentasPaginadasAsync(usuarioId, metodoPago, fechaInicio, fechaFin, pageNumber, pageSize);



        }







        public async Task<DetallesVenta> GetVentasById(int id)
        {

            return await _ventasRepository.GetById(id);



        }


        public async Task<IEnumerable<DetallesVenta>> GetVentasDelDia(int usuarioId, DateTime fechaActual)
        {
           
            return await _ventasRepository.GetByDayAsync(usuarioId, fechaActual);
        }






        public async Task AgregarVenta(DetallesVenta venta)
        {
            await _ventasRepository.AddAsync(venta);
          


        }


        public async Task EditarVenta( DetallesVenta venta)
        {

            await _ventasRepository.UpdateAsync(venta);
         


        }



       
        public async Task EliminarVenta(int id)
        {

            await _ventasRepository.DeleteAsync(id);
            


        }










    }
}
