using System;
using System.Collections.Generic;

namespace WpfExamGrebenukov.Models;

public partial class PartnerProduct
{
    public int SalesId { get; set; }

    public int ProductId { get; set; }

    public int PartnerId { get; set; }

    public int Count { get; set; }

    public DateOnly DataSales { get; set; }

    public virtual Partner Partner { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
