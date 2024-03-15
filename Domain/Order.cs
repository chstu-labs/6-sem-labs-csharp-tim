using System;

namespace Lab4;

public class Order : EntityBase
{
    public virtual string? CustomerName { get; set; }
    public virtual string? PizzaSize { get; set; }
    public virtual string CustomerSex { get; set; }
    public virtual int Price { get; set; }
}

