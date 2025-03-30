using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;
using MVC_GestionVerdu.Services.Interfaces;

namespace MVC_GestionVerdu.Services
{
    public class MetodoPagosServices: IMetodoPagoService
    {
        private readonly IMetodoPagosRepository _metodoPagosRepository;


        public MetodoPagosServices(IMetodoPagosRepository metodoPagosRepository)
        {

            _metodoPagosRepository = metodoPagosRepository;

        }

        public async Task<IEnumerable<MetodoPago>> GetMetodoPagos()
        {


            return await _metodoPagosRepository.GetAllasync();


        }






    }
}
