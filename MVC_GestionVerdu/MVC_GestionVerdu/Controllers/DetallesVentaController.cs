using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services;

namespace MVC_GestionVerdu.Controllers
{
    public class DetallesVentaController : Controller
    {

        private readonly IVentaService _ventaService;
        private readonly IMetodoPagoService _metodoPagoService;

        public DetallesVentaController(IVentaService ventaService, IMetodoPagoService metodoPagoService)
        {

            _ventaService = ventaService;
            _metodoPagoService = metodoPagoService; 
        }

        public async Task<IActionResult> Index()
        {

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            
            var ventas = await _ventaService.GetVentas(usuarioId);

           
            return View(ventas);
        }



        public async Task<IActionResult> AgregarVenta()
        {
            var model = new DetallesVenta
            {
                Fecha = DateTime.Today // Asigna la fecha de hoy
            };

            var metodoPagos = await _metodoPagoService.GetMetodoPagos();


            ViewBag.MetodoPago= metodoPagos;


            return View(model);


        }



        [HttpPost]
        public async Task<IActionResult> AgregarVenta(DetallesVenta venta)
        {


            venta.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));




            await _ventaService.AgregarVenta(venta);

            TempData["MensajeVenta"] = "Venta agregada correctamente.";

            return RedirectToAction("Index");






        }


        

        public async Task<IActionResult> Editar(int id)
        {


            var venta = await _ventaService.GetVentasById(id);
            var metodoPago = await _metodoPagoService.GetMetodoPagos();

            ViewBag.MetodoPago = metodoPago;


            return View(venta);

        }

        [HttpPost]

        public async Task<IActionResult> Editar(DetallesVenta venta) {


            var ventaEditada = await _ventaService.GetVentasById(venta.IdDetalleVenta);


            ventaEditada.MetodoPagoId = venta.MetodoPagoId;
            ventaEditada.Concepto= venta.Concepto;
            ventaEditada.Fecha= venta.Fecha;
            ventaEditada.Monto= venta.Monto;



            ViewBag.MetodoPago = await _metodoPagoService.GetMetodoPagos();


            await _ventaService.EditarVenta(ventaEditada);



            TempData["MensajeVentaEditado"] = "Venta editada correctamente.";


            return View(venta);
        
        
        
        
        
        
        
        }






        [HttpPost]


        public async Task<IActionResult> Eliminar(int id) { 
        
            await _ventaService.EliminarVenta(id);

            return RedirectToAction("Index");




        }








    }
}
