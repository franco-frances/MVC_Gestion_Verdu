using System;
using System.Collections.Generic;

namespace MVC_GestionVerdu.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();
}
