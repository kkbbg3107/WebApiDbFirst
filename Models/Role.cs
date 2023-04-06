using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Name { get; set; }
}
