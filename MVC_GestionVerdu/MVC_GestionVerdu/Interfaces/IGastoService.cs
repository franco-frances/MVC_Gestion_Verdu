using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IGastoService
    {

        Task<IEnumerable<Gastos>> GetGastos(int idUsuario);
        Task<Gastos> GetGastoById(int id);
        Task AgregarGasto( Gastos gasto);
        Task EditarGasto( Gastos gasto);
        Task EliminarGasto(int id);



    }
}
