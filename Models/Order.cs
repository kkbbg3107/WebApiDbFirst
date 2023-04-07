using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Order
{
    public int RowId { get; set; }

    public int? OrderId { get; set; }

    public string? CustNo { get; set; }

    public decimal? Price { get; set; }

    public virtual Customer? CustNoNavigation { get; set; }
}
