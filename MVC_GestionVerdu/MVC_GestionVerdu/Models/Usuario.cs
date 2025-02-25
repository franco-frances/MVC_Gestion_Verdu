using System;
using System.Collections.Generic;

namespace MVC_GestionVerdu.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? NickName { get; set; }

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();

    public virtual ICollection<Gastos> Gastos { get; set; } = new List<Gastos>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
