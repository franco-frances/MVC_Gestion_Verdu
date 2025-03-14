using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IVentaService _ventaService;
        private readonly IGastoService _gastoService;
        private readonly IMetodoPagoService _metodoPagoService;

        public DashBoardController(IProductoService productoService, ICategoriaService categoriaService, IVentaService ventaService, IGastoService gastoService, IMetodoPagoService metodoPagoService)
        {
            
            _productoService = productoService;
            _categoriaService= categoriaService;
            _ventaService = ventaService;
            _gastoService = gastoService;
            _metodoPagoService = metodoPagoService;
        }



        public async Task<IActionResult> Index()
        {

            var fechaActual = DateTime.Today;

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            var productos= await _productoService.ListarProductos(usuarioId);

            var ingresoHoy = await _ventaService.GetVentasDelDia(usuarioId, fechaActual);

            var totalIngresosHoy = ingresoHoy.Sum(i => i.Monto);

            var gastosHoy = await _gastoService.GetGastosDelDia(usuarioId, fechaActual);
           
            var totalGastosHoy= gastosHoy.Sum(i => i.Monto);

            




            var categorias = await _categoriaService.GetCategorias();
            var metodoPagos = await _metodoPagoService.GetMetodoPagos();


            ViewBag.IngresosHoy = ingresoHoy;
            ViewBag.TotalIngresosHoy = totalIngresosHoy;
            ViewBag.GastosHoy = gastosHoy;
            ViewBag.totalGastosHoy = totalGastosHoy;
            ViewBag.Categorias = categorias;
            ViewBag.MetodoPago = metodoPagos;
            ViewBag.TotalHoy = totalIngresosHoy - totalGastosHoy;


            TempData["Mensaje"] = "Producto agregado correctamente.";


            return View(productos);
        }



        public async Task<IActionResult> Editar(int id)
        {


            var producto = await _productoService.GetProducto(id);
            var categoria = await _categoriaService.GetCategorias();

            ViewBag.Categorias = categoria;

            
            return View(producto);




        }



        [HttpPost]

        public async Task<IActionResult> Editar(Producto producto) {


            var produEditado = await _productoService.GetProducto(producto.IdProductos);

            produEditado.Descripcion= producto.Descripcion;
            produEditado.CategoriaId= producto.CategoriaId;
            produEditado.PrecioCajon= producto.PrecioCajon;
            produEditado.PesoCajon= producto.PesoCajon;
            produEditado.MargenGanancia= producto.MargenGanancia;
            produEditado.PrecioCosto= producto.PrecioCosto;
            produEditado.PrecioFinal= producto.PrecioFinal;
            


            //pasa las categorias para que sean cargadas en select
            ViewBag.Categorias = await _categoriaService.GetCategorias();



            await _productoService.EditarProducto(produEditado);



            TempData["MensajeEditar"] = "Producto editado correctamente.";

            return View(producto);


        }


        [HttpPost]

        public async Task<IActionResult> Eliminar(int id)
        {

            await _productoService.EliminarProducto(id);
            
            
           
            return RedirectToAction("Index");




        }




    }
}
