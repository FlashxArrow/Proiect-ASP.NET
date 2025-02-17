using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DBObjects;

public partial class Franchise
{
    public int IdFranchises { get; set; }

    public string FranchisesName { get; set; } = null!;

    public string? Headquarters { get; set; }

    public decimal? Budget { get; set; }

    public DateOnly? DateFoundation { get; set; }

    public int? IdAuthor { get; set; }

    public decimal? AverageRating { get; set; }

    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();

    public virtual Author? IdAuthorNavigation { get; set; }
}
