using System.ComponentModel.DataAnnotations;

namespace MVC_GestionVerdu.ViewModels
{
    public class ProductoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoriaId { get; set; }

        public string CategoriaNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio del cajón es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del cajón debe ser mayor a 0")]
        public decimal PrecioCajon { get; set; }

        [Required(ErrorMessage = "El peso del cajón es obligatorio")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El peso del cajón debe ser mayor a 0")]
        public decimal PesoCajon { get; set; }

        [Required(ErrorMessage = "El margen de ganancia es obligatorio")]
        [Range(0, 100, ErrorMessage = "El margen de ganancia debe estar entre 0 y 100")]
        public decimal MargenGanancia { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de costo debe ser un valor positivo")]
        public decimal PrecioCosto { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio final debe ser un valor positivo")]
        public decimal PrecioFinal { get; set; }

        // Propiedad para cargar las categorías en el select
        public List<CategoriaViewModel> Categorias{ get; set; } = new List<CategoriaViewModel>();

    }
}
