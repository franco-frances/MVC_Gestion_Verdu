using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IMetodoPagoService
    {

        Task<IEnumerable<MetodoPago>> GetMetodoPagos();


    }
}
