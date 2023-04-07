using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class EmployeeDetail
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public decimal? Salary { get; set; }

    public string? Email { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
