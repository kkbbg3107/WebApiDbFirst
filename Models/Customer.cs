using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Customer
{
    public int CustId { get; set; }

    public string? CustName { get; set; }

    public string? CustCode { get; set; }

    public string CustNo { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
