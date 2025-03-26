using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IGastoService
    {
        Task<(IEnumerable<Gastos> gastos, int totalRegistros,decimal totalMonto)> GetGastosPaginados(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize);
        Task<IEnumerable<Gastos>> GetGastos(int idUsuario);
        Task<IEnumerable<Gastos>> GetGastosDelDia(int usuarioId, DateTime fechaActual);
        Task<Gastos> GetGastoById(int id);
        Task AgregarGasto( Gastos gasto);
        Task EditarGasto( Gastos gasto);
        Task EliminarGasto(int id);



    }
}
