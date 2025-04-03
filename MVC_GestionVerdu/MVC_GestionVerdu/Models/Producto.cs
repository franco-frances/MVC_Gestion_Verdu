using System;
using System.Collections.Generic;

namespace MVC_GestionVerdu.Models;

public partial class Producto
{
    public int IdProductos { get; set; }

    public string Descripcion { get; set; } = null!;

    public int CategoriaId { get; set; }

    public int UsuarioId { get; set; }

    public decimal PrecioCajon { get; set; }

    public decimal PesoCajon { get; set; }

    public decimal MargenGanancia { get; set; }

    public decimal PrecioCosto { get; set; }
    public decimal? PrecioFinal { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
