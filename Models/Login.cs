using System;
using System.Collections.Generic;

namespace WebApiDBFirst.Models;

public partial class Login
{
    public int Id { get; set; }

    public string MemberName { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;
}
