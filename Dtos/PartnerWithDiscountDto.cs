using WpfExamGrebenukov.Models;

namespace WpfExamGrebenukov.Dtos;

public class PartnerWithDiscountDto : Partner
{
    public PartnerWithDiscountDto(Partner partner)
    {
        PartnerId = partner.PartnerId;
        Title = partner.Title;
        Director = partner.Director;
        EmailPartner = partner.EmailPartner;
        PhonePartner = partner.PhonePartner;
        AddressPartner = partner.AddressPartner;
        Inn = partner.Inn;
        Rating = partner.Rating;
        PartnerTypes = partner.PartnerTypes;
        PartnerProducts = partner.PartnerProducts;
    }

    public int TotalSales { get; set; }
    public int Discount { get; set; }
}
