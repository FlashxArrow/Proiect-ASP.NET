using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DBObjects;

public partial class Comic
{
    public int IdComic { get; set; }

    public int? IdAuthor { get; set; }

    public int? IdFranchises { get; set; }

    public string ComicName { get; set; } = null!;

    public decimal? Rating { get; set; }

    public decimal Price { get; set; }

    public int? Stock { get; set; }

    public string? ShortDescription { get; set; }

    public string? ImageUrl { get; set; } 

    public virtual Author? IdAuthorNavigation { get; set; }

    public virtual Franchise? IdFranchisesNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
