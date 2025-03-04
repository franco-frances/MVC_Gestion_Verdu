using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IVentaService
    {

        Task<IEnumerable<DetallesVenta>> GetVentas(int idUsuario);
        Task<DetallesVenta> GetVentasById(int id);
        Task AgregarVenta(DetallesVenta venta);
        Task EditarVenta(DetallesVenta venta);
        Task EliminarVenta(int id);


    }
}
