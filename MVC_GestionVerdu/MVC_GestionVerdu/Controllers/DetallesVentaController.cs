﻿using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Attributes;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services;
using MVC_GestionVerdu.Services.Interfaces;

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
        
        [SessionAuthorize]
        public async Task<IActionResult> Index()
        {

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            ViewBag.MetodoPago= await _metodoPagoService.GetMetodoPagos();


            return View();
        }

        [SessionAuthorize]
        public async Task<IActionResult> ListadoPaginado(string? metodoPago, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber = 1, int pageSize = 10) {

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            var (ventas, totalRegistros, totalMonto) = await _ventaService.GetVentasPaginadas(usuarioId, metodoPago, fechaInicio, fechaFin, pageNumber, pageSize);

            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalMonto = totalMonto;

            return PartialView("_ListadoVentas", ventas);

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


        [SessionAuthorize]
        [HttpPost]
        public async Task<IActionResult> AgregarVenta(DetallesVenta venta, string origen)
        {

            if (origen == "ingresosRapidos") {

                venta.Concepto = "Varios";
            
            }

            venta.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));




            await _ventaService.AgregarVenta(venta);

            if (origen == "ingresosRapidos")
            {

                return RedirectToAction("Index","DashBoard");


            }


            TempData["MensajeIngresos"] = "Ingreso agregado correctamente.";
            TempData["TipoMensajeIngresos"] = "success"; // Para SweetAlert


            return RedirectToAction("Index");






        }


        

        public async Task<IActionResult> Editar(int id)
        {


            var venta = await _ventaService.GetVentasById(id);
            var metodoPago = await _metodoPagoService.GetMetodoPagos();

            ViewBag.MetodoPago = metodoPago;


            return View(venta);

        }
        [SessionAuthorize]
        [HttpPost]

        public async Task<IActionResult> Editar(DetallesVenta venta) {


            var ventaEditada = await _ventaService.GetVentasById(venta.IdDetalleVenta);

            if (ventaEditada == null)
            {
                TempData["MensajeIngresos"] = "El Ingreso no existe.";
                TempData["TipoMensajeIngresos"] = "error";
                return RedirectToAction("Index");
            }


            try
            {
                ventaEditada.MetodoPagoId = venta.MetodoPagoId;
                ventaEditada.Concepto= venta.Concepto;
                ventaEditada.Fecha= venta.Fecha;
                ventaEditada.Monto= venta.Monto;



                ViewBag.MetodoPago = await _metodoPagoService.GetMetodoPagos();


                await _ventaService.EditarVenta(ventaEditada);

                TempData["MensajeIngresos"] = "Ingreso editado correctamente.";
                TempData["TipoMensajeIngresos"] = "success";


            }
            catch (Exception)
            {
                TempData["MensajeIngresos"] = "El Ingreso no existe.";
                TempData["TipoMensajeIngresos"] = "error";

                return RedirectToAction("Index");
            }


                return RedirectToAction("Index");

        
        }



        [HttpPost]


        public async Task<IActionResult> Eliminar(int id) {


            var ingreso = await _ventaService.GetVentas(id);
            if (ingreso == null)
            {
                return Json(new { success = false, message = "El ingreso no existe." });
            }
            
            try
            {
                await _ventaService.EliminarVenta(id);
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el ingreso: " + ex.Message });
            }


                return Json(new { success = true, message = "Ingreso eliminado correctamente." });
            

        }








    }
}
