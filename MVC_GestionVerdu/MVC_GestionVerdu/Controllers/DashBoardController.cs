﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_GestionVerdu.Attributes;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MVC_GestionVerdu.ViewModels;

namespace MVC_GestionVerdu.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IVentaService _ventaService;
        private readonly IGastoService _gastoService;
        private readonly IMetodoPagoService _metodoPagoService;

        public DashBoardController(IProductoService productoService, ICategoriaService categoriaService, 
            IVentaService ventaService, IGastoService gastoService, IMetodoPagoService metodoPagoService)
        {
            
            _productoService = productoService;
            _categoriaService= categoriaService;
            _ventaService = ventaService;
            _gastoService = gastoService;
            _metodoPagoService = metodoPagoService;
        }


        [SessionAuthorize]
        public async Task<IActionResult> Index()
        {
            try
            {

                // Recupera datos necesarios
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
                var fechaActual = DateTime.Today;

                // Lista de productos
                var productos = await _productoService.ListarProductos(usuarioId);

                // Ventas y gastos del día
                var ventasHoy = await _ventaService.GetVentasDelDia(usuarioId, fechaActual);
                var gastosHoy = await _gastoService.GetGastosDelDia(usuarioId, fechaActual);
                

                // Calcular totales
                var totalVentasHoy = ventasHoy.Sum(v => v.Monto);
                var totalGastosHoy = gastosHoy.Sum(g => g.Monto);

                // Obtener todas las categorías
                var categorias = await _categoriaService.GetCategorias();
                var categoriasViewModel = categorias.Select(c => new CategoriaViewModel
                {
                    Id = c.IdCategoria,
                    Descripcion = c.Descripcion
                }).ToList();

                // Obtener todos los métodos de pago
                var metodosPago = await _metodoPagoService.GetMetodoPagos();
                var metodosPagoViewModel = metodosPago.Select(m => new MetodoPagoViewModel
                {
                    Id = m.IdMetodoPago,
                    Descripcion = m.Descripcion
                }).ToList();



                // Mapeo de entidades a ViewModels para productos
                var productosViewModel = productos.Select(p => new ProductoViewModel
                {
                    Id = p.IdProductos, // Asegúrate de que en tu entidad la propiedad se llame 'Id'
                    Descripcion = p.Descripcion,
                    CategoriaId = p.CategoriaId,
                    CategoriaNombre = p.Categoria.Descripcion, // O mapea según corresponda
                    PrecioCajon = p.PrecioCajon,
                    PesoCajon = p.PesoCajon,
                    MargenGanancia = p.MargenGanancia,
                    PrecioCosto = p.PrecioCosto,
                    PrecioFinal = p.PrecioFinal ?? 0m
                }).ToList();

                // Aquí deberás mapear también gastos y ventas si los necesitas en el dashboard:
                var gastosViewModel = gastosHoy.Select(g => new GastoViewModel
                {
                    Id = g.IdGasto,  // Asegúrate de mapear la propiedad correcta, por ejemplo g.IdGasto o similar
                    Fecha = g.Fecha,
                    Concepto = g.Concepto,
                    Monto = g.Monto
                }).ToList();

                var ventasViewModel = ventasHoy.Select(v => new VentaViewModel
                {
                    Id = v.IdDetalleVenta, // Mapea de acuerdo a tu entidad
                    MetodoPagoId=v.MetodoPagoId,
                    MetodoPagoNombre= v.MetodoPago.Descripcion,
                    Concepto= v.Concepto,
                    Monto= v.Monto,
                    Fecha = v.Fecha
                }).ToList();

                // Crear y poblar el DashboardViewModel
                var dashboardViewModel = new DashBoardViewModel
                {
                    TotalVentas = totalVentasHoy,
                    TotalGastos = totalGastosHoy,
                    CantidadProductos = productos.Count(),
                    Productos = productosViewModel,
                    GastosRecientes = gastosViewModel,
                    VentasRecientes = ventasViewModel,
                    Categorias = categoriasViewModel,  // Agregamos las categorías
                    MetodosPago = metodosPagoViewModel
                };

                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { mensaje = ex.Message });
            }
        }




        [SessionAuthorize]
        [HttpPost]
        public async Task<IActionResult> AgregarProducto(ProductoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensajeProductos"] = "Hay errores en los datos ingresados.";
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }

            try
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

                // Mapear el ViewModel al modelo de dominio Producto
                var producto = new Producto
                {
                    // Si es un producto nuevo, normalmente IdProductos será 0 o se asignará automáticamente
                    
                    Descripcion = model.Descripcion,
                    CategoriaId = model.CategoriaId,
                    PrecioCajon = model.PrecioCajon,
                    PesoCajon = model.PesoCajon,
                    MargenGanancia = model.MargenGanancia,
                    PrecioCosto = model.PrecioCosto,
                    PrecioFinal = model.PrecioFinal,
                    UsuarioId = usuarioId
                };

                await _productoService.AgregarProducto(producto);

                TempData["MensajeProductos"] = "Producto agregado correctamente.";
                TempData["TipoMensajeProductos"] = "success";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeProductos"] = ex.Message;
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }
        }





        public async Task<IActionResult> Editar(int id)
        {


            var producto = await _productoService.GetProducto(id);
            var categoria = await _categoriaService.GetCategorias();

            ViewBag.Categorias = categoria;

            
            return View(producto);




        }



        [HttpPost]

        public async Task<IActionResult> Editar(ProductoViewModel model) {


            if (!ModelState.IsValid)
            {
                TempData["MensajeProductos"] = "Hay errores en los datos ingresados.";
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }



            var produEditado = await _productoService.GetProducto(model.Id);


            if (produEditado == null)
            {
                TempData["MensajeProductos"] = "El Producto no existe.";
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }

            try
            {

                produEditado.Descripcion= model.Descripcion;
                produEditado.CategoriaId= model.CategoriaId;
                produEditado.PrecioCajon= model.PrecioCajon;
                produEditado.PesoCajon= model.PesoCajon;
                produEditado.MargenGanancia= model.MargenGanancia;
                produEditado.PrecioCosto= model.PrecioCosto;
                produEditado.PrecioFinal= model.PrecioFinal;
            
            
               


                await _productoService.EditarProducto(produEditado);


                TempData["MensajeProductos"] = "Producto editado correctamente.";
                TempData["TipoMensajeProductos"] = "success";

            }
            catch (Exception)
            {
                TempData["MensajeProductos"] = "El Producto no existe.";
                TempData["MensajeProductos"] = "error";

                return RedirectToAction("Index");

            }


            return RedirectToAction("Index");


        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id){
                    
            var producto = await _productoService.GetProducto(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "El producto no existe." });
            }
            try
            {
                await _productoService.EliminarProducto(id);
                return Json(new { success = true, message = "Producto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el producto: " + ex.Message });
            }
                
        
        
        }

        [SessionAuthorize]
        [HttpPost]
        public async Task<IActionResult> AgregarVentaRapida([Bind(Prefix = "VentaRapida")] VentaViewModel model, string origen)
        {

            
                model.Concepto = "Varios";
                model.Fecha = DateTime.Today;
            

            try
            {


                int UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));


                var venta = new DetallesVenta
                {

                    UsuarioId = UsuarioId,
                    Concepto = model.Concepto,
                    Fecha = model.Fecha,
                    Monto = model.Monto,
                    MetodoPagoId = model.MetodoPagoId


                };



                await _ventaService.AgregarVenta(venta);

                // Ventas y gastos del día
                var ventasHoy = await _ventaService.GetVentasDelDia(UsuarioId, model.Fecha);
                var gastosHoy = await _gastoService.GetGastosDelDia(UsuarioId, model.Fecha);


                // Calcular totales
                var totalVentasHoy = ventasHoy.Sum(v => v.Monto);
                var totalGastosHoy = gastosHoy.Sum(g => g.Monto);


                var BalanceHoy = totalVentasHoy - totalGastosHoy;

                return Json(new
                {
                    exito = true,
                    fecha = venta.Fecha.ToString("yyyy-MM-dd"),
                    concepto = venta.Concepto,
                    monto = venta.Monto,
                    montoFormateado = venta.Monto.ToString("C"),
                    totalFormateado = totalVentasHoy.ToString("C"),
                    balanceHoyFormateado = BalanceHoy.ToString("C")
                });
            }
            catch (Exception ex)
            {

                return Json(new { exito = false, mensaje = ex.Message });

            }


        }


        [SessionAuthorize]
        [HttpPost]
        public async Task<IActionResult> AgregarGastoRapido([Bind(Prefix = "GastoRapido")] GastoViewModel model, string origen)
        {
            model.Fecha = DateTime.Today;

            try
            {
                int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

                var gasto = new Gastos
                {
                    Concepto = model.Concepto,
                    Fecha = model.Fecha,
                    Monto = model.Monto,
                    UsuarioId = usuarioId
                };

                await _gastoService.AgregarGasto(gasto);

                // Obtener ventas y gastos del día
                var ventasHoy = await _ventaService.GetVentasDelDia(usuarioId, model.Fecha);
                var gastosHoy = await _gastoService.GetGastosDelDia(usuarioId, model.Fecha);

                var totalVentasHoy = ventasHoy.Sum(v => v.Monto);
                var totalGastosHoy = gastosHoy.Sum(g => g.Monto);
                var balanceHoy = totalVentasHoy - totalGastosHoy;

                return Json(new
                {
                    exito = true,
                    fecha = gasto.Fecha.ToString("yyyy-MM-dd"),
                    concepto = gasto.Concepto,
                    monto = gasto.Monto,
                    montoFormateado = gasto.Monto.ToString("C"),
                    totalFormateado = totalGastosHoy.ToString("C"),
                    balanceHoyFormateado = balanceHoy.ToString("C")
                });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = ex.Message });
            }
        }






        [SessionAuthorize]
        [HttpGet]
        public async Task<IActionResult> DescargarPdfProductos()
        {
            // Obtén el usuario actual (ejemplo usando Session)
            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            var productos = await _productoService.ListarProductos(usuarioId);

            // Crea un nuevo documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Lista de Productos";

            // Agrega una página y obtiene el objeto gráfico
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Define las fuentes
            XFont tituloFuente = new XFont("Verdana", 20, XFontStyleEx.Bold);
            XFont textoFuente = new XFont("Verdana", 12, XFontStyleEx.Regular);

            // Dibujar el título centrado
            gfx.DrawString("Lista de Productos", tituloFuente, XBrushes.Black,
                           new XRect(0, 20, page.Width, 40), XStringFormats.TopCenter);

            // Posición inicial para listar los productos
            int posY = 60;
            int margenIzquierdo = 40;
            int saltoLinea = 20;

            // Iterar sobre cada producto
            foreach (var producto in productos)
            {
                string linea = $"{producto.Descripcion} - {(producto.PrecioFinal.HasValue ? producto.PrecioFinal.Value.ToString("C") : "N/A")}";
                gfx.DrawString(linea, textoFuente, XBrushes.Black, margenIzquierdo, posY);

                posY += saltoLinea;

                // Si se excede el alto de la página, crea una nueva página
                if (posY > page.Height - 40)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    posY = 40; // reinicia el margen superior en la nueva página
                }
            }

            // Guardar el documento en un MemoryStream
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                // Retorna el PDF para su descarga
                return File(stream.ToArray(), "application/pdf", "ListaProductos.pdf");
            }
        }





    }
}
