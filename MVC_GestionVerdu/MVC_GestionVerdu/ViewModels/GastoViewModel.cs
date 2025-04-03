using System.ComponentModel.DataAnnotations;

namespace MVC_GestionVerdu.ViewModels
{
    public class GastoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha del gasto es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El concepto es obligatorio")]
        [StringLength(100, ErrorMessage = "El concepto no puede superar los 100 caracteres")]
        public string Concepto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

    }
}
