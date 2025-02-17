using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DBObjects;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string AuthorName { get; set; } = null!;

    public DateOnly DateBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public decimal? Salary { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();

    public virtual ICollection<Franchise> Franchises { get; set; } = new List<Franchise>();
}
