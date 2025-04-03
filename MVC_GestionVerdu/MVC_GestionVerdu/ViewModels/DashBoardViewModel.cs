namespace MVC_GestionVerdu.ViewModels
{
    public class DashBoardViewModel
    {
        public decimal TotalVentas { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance => TotalVentas - TotalGastos;
        public int CantidadProductos { get; set; }

        public List<ProductoViewModel> Productos { get; set; } = new List<ProductoViewModel>();
        public List<GastoViewModel> GastosRecientes { get; set; } = new List<GastoViewModel>();
        public List<VentaViewModel> VentasRecientes { get; set; } = new List<VentaViewModel>();
        public List<CategoriaViewModel> Categorias { get; set; } = new List<CategoriaViewModel>();
        public List<MetodoPagoViewModel> MetodosPago { get; set; } = new List<MetodoPagoViewModel>();


    }
}
