using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Services
{


    public class ProductoServices:IProductoService
    {

        private readonly IProductoRepository _productoRepository;

        public ProductoServices(IProductoRepository productoRepository)
        {

            _productoRepository = productoRepository;

        }


        public async Task<IEnumerable<Producto>> ListarProductos(int usuarioId)
        {
            return await _productoRepository.GetAllAsync(usuarioId);
        }


        public async Task<Producto> GetProducto(int id)
        {

           
            return await _productoRepository.GetbyIdAsync(id);


        }

        public async Task AgregarProducto(Producto producto)
        {
            await _productoRepository.AddAsync(producto);
        }


        public async Task EditarProducto(Producto producto)
        {

            await _productoRepository.UpdateAsync(producto);
            

        }

        public async Task EliminarProducto(int id)
        {

            await _productoRepository.DeleteAsync(id);
            

        }



    }
}
