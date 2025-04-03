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


            var producto = await _productoRepository.GetbyIdAsync(id);

            if (producto == null)
            {
                throw new KeyNotFoundException($"No se encontró un producto con el ID {id}.");
            }

            return producto;


        }

        public async Task AgregarProducto(Producto producto)
        {

            if (string.IsNullOrWhiteSpace(producto.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");



            bool existe = await _productoRepository.ExistsAsync(producto.UsuarioId, producto.Descripcion);

            if (existe)
            {
                throw new Exception("El producto ya existe.");
            }

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
