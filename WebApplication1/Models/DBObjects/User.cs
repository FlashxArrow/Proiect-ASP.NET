using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DBObjects;

public partial class User 
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Age { get; set; }

    public decimal? Budget { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
