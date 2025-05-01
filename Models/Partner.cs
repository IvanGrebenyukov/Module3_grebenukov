using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfExamGrebenukov.Models;

public partial class Partner
{
    public int PartnerId { get; set; }

    public int PartnerTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string EmailPartner { get; set; } = null!;

    public string PhonePartner { get; set; } = null!;

    public string AddressPartner { get; set; } = null!;

    public long Inn { get; set; }

    public int Rating { get; set; }

    

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();

    public virtual PartnerType PartnerTypes { get; set; } = null!;
}
