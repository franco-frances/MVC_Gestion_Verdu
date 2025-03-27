using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

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


        [HttpPost]

        public async Task<IActionResult> AgregarProducto(Producto producto)
        {

            producto.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));




            await _productoService.AgregarProducto(producto);

            TempData["MensajeProductos"] = "Producto editado correctamente.";
            TempData["TipoMensajeProductos"] = "success";



            return RedirectToAction("Index");



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


            if (produEditado == null)
            {
                TempData["MensajeProductos"] = "El Producto no existe.";
                TempData["TipoMensajeProductos"] = "error";
                return RedirectToAction("Index");
            }

            try
            {

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
