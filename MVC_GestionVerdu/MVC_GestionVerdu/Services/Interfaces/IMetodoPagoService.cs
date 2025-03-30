using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services.Interfaces
{
    public interface IMetodoPagoService
    {

        Task<IEnumerable<MetodoPago>> GetMetodoPagos();


    }
}
