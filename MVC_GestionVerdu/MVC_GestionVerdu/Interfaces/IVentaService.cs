using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IVentaService
    {
        Task<(IEnumerable<DetallesVenta> ventas, int totalRegistros, decimal totalMonto)> GetVentasPaginadas(int usuarioId, string? metodoPago, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize);
        Task<IEnumerable<DetallesVenta>> GetVentas(int idUsuario);
        Task<IEnumerable<DetallesVenta>> GetVentasDelDia(int usuarioId, DateTime fechaActual);
        Task<DetallesVenta> GetVentasById(int id);
        Task AgregarVenta(DetallesVenta venta);
        Task EditarVenta(DetallesVenta venta);
        Task EliminarVenta(int id);


    }
}
