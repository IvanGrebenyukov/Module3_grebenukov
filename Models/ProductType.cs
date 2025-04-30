using System;
using System.Collections.Generic;

namespace WpfExamGrebenukov.Models;

public partial class ProductType
{
    public int TypeProductId { get; set; }

    public string Title { get; set; } = null!;

    public decimal CoefTypeProduct { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
