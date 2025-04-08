using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Attributes;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.ViewModels;

namespace MVC_GestionVerdu.Controllers
{
    public class GastosController : Controller
    {

        private readonly IGastoService _gastoService;

        public GastosController(IGastoService gastoService)
        {
            
            _gastoService = gastoService;
            
        }
        [SessionAuthorize]
        public async Task<IActionResult> ListadoPaginado(DateTime? fechaInicio, DateTime? fechaFin, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            
            
                var (gastos, totalRegistros, totalMonto) = await _gastoService.GetGastosPaginados(usuarioId, fechaInicio, fechaFin, pageNumber, pageSize);


                // Mapear las entidades de Gasto a GastoViewModel
                var gastosViewModel = gastos.Select(g => new GastoViewModel
                {
                    Id = g.IdGasto,      // Asegurate de mapear la propiedad correcta
                    Fecha = g.Fecha,
                    Concepto = g.Concepto,
                    Monto = g.Monto
                }).ToList();





                // Si deseas devolver un PartialView:
                ViewBag.TotalRegistros = totalRegistros;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalMonto = totalMonto;
                
                return PartialView("_ListadoGastos", gastosViewModel);

                // O bien, devolver JSON:
                // return Json(new { gastos, totalRegistros });

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { mensaje = ex.Message });
            }
            
            
        }




        [SessionAuthorize]
        public async Task<IActionResult> Index()
        {       
            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            
                                                       

            return View();
        }


      
        
        [SessionAuthorize]
        [HttpPost]
        public async Task<IActionResult> AgregarGasto(GastoViewModel model)
        {

           

            try
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));



                var gasto = new Gastos {

                    Concepto = model.Concepto,
                    Fecha = model.Fecha,
                    Monto = model.Monto,
                    UsuarioId = usuarioId


                };
                
                
                await _gastoService.AgregarGasto(gasto);
                
                
                              
                
                TempData["MensajeGastos"] = "Gasto agregado correctamente.";
                TempData["TipoMensajeGastos"] = "success"; // Para SweetAlert

                return RedirectToAction("Index");
                
            


            }
            catch (Exception ex)
            {
                TempData["MensajeProductos"] = ex.Message;
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }
            









        }



        public async Task<IActionResult> Editar(int id) {


            var gasto = await _gastoService.GetGastoById(id);

            return View(gasto);
        
        
        }


        [HttpPost]
        public async Task<IActionResult> Editar(GastoViewModel model)
        {
            var gastoEditado = await _gastoService.GetGastoById(model.Id);

            if (gastoEditado == null)
            {
                TempData["MensajeGastos"] = "El gasto no existe.";
                TempData["TipoMensajeGastos"] = "error";
                return RedirectToAction("Index");
            }

            try
            {
                gastoEditado.Concepto = model.Concepto;
                gastoEditado.Fecha = model.Fecha;
                gastoEditado.Monto = model.Monto;

                await _gastoService.EditarGasto(gastoEditado);

                TempData["MensajeGastos"] = "Gasto editado correctamente.";
                TempData["TipoMensajeGastos"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeGastos"] = "Error al editar el gasto: " + ex.Message;
                TempData["TipoMensajeGastos"] = "error";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var gasto = await _gastoService.GetGastoById(id);

            if (gasto == null)
            {
                return Json(new { success = false, message = "El gasto no existe." });
            }

            try
            {
                await _gastoService.EliminarGasto(id);
                return Json(new { success = true, message = "Gasto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el gasto: " + ex.Message });
            }
        }




    }
}
