using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ListarProductos(int usuarioId);
        Task<Producto> GetProducto(int id);
        Task AgregarProducto(Producto producto);
        Task EditarProducto(Producto producto);
        Task EliminarProducto(int id);

    }
}
