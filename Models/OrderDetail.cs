using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int UnitPrice { get; set; }

    public short Quantity { get; set; }

    public int ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
