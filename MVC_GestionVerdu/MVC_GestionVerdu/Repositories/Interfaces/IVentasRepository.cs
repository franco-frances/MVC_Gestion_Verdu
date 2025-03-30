using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IVentasRepository
    {
        Task AddAsync(DetallesVenta venta);
        Task DeleteAsync(int id);
        Task<IEnumerable<DetallesVenta>> GetAllAsync(int usuarioId);
        Task<IEnumerable<DetallesVenta>> GetByDayAsync(int usuarioId, DateTime fechaActual);
        Task<DetallesVenta> GetById(int id);
        Task<(IEnumerable<DetallesVenta> ventas, int totalRegistros, decimal totalMonto)> GetVentasPaginadasAsync(int usuarioId, string? metodoPago, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize);
        Task UpdateAsync(DetallesVenta venta);
    }
}