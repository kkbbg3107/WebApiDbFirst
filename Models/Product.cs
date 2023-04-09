using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
