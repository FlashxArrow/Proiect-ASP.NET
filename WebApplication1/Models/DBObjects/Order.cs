using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DBObjects;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? IdUser { get; set; }

    public int? IdComic { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? ShippingAddress { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public string? ShippingMethod { get; set; }

    public string TransactionType { get; set; } = null!;

    public virtual Comic? IdComicNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
