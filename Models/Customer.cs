using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
