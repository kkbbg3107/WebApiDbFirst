using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Rank { get; set; }
}
