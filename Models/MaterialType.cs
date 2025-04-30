using System;
using System.Collections.Generic;

namespace WpfExamGrebenukov.Models;

public partial class MaterialType
{
    public int MaterialTypeId { get; set; }

    public string MaterialType1 { get; set; } = null!;

    public string PercentageOfDefects { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
