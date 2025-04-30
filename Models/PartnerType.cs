using System;
using System.Collections.Generic;

namespace WpfExamGrebenukov.Models;

public partial class PartnerType
{
    public int PartnerTypeId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
