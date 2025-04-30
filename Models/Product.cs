using System;
using System.Collections.Generic;

namespace WpfExamGrebenukov.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int TypeId { get; set; }

    public string Title { get; set; } = null!;

    public int Article { get; set; }

    public decimal MinCostForPartner { get; set; }

    public int MaterialId { get; set; }

    public virtual MaterialType MaterialTypes { get; set; } = null!;

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual ProductType ProductTypes { get; set; } = null!;
}
