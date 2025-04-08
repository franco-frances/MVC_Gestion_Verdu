using System.ComponentModel.DataAnnotations;

namespace MVC_GestionVerdu.ViewModels
{
    public class VentaViewModel
    {

        // Mapea IdDetalleVenta a Id
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un método de pago.")]
        public int MetodoPagoId { get; set; }

        // Propiedad opcional para mostrar el nombre del método de pago
        public string MetodoPagoNombre { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "El concepto no puede superar los 200 caracteres.")]
        public string? Concepto { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

         public List<MetodoPagoViewModel> Metodo { get; set; } = new List<MetodoPagoViewModel>();
    }
}
