using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Models;

namespace WpfExamGrebenukov.Services
{
    public class PartnerService
    {
        private readonly AppDbContext _context;

        public PartnerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Partner> GetPartnersWithDiscount()
        {
            return _context.Partners
                .Include(p => p.PartnerTypes)
                .Include(p => p.PartnerProducts)
                .AsEnumerable()
                .Select(p => new Partner
                {
                    PartnerId = p.PartnerId,
                    PartnerTypeId = p.PartnerTypeId,
                    Title = p.Title,
                    Director = p.Director,
                    EmailPartner = p.EmailPartner,
                    PhonePartner = p.PhonePartner,
                    AddressPartner = p.AddressPartner,
                    Inn = p.Inn,
                    Rating = p.Rating,
                    PartnerProducts = p.PartnerProducts,
                    PartnerTypes = p.PartnerTypes,
                    Discount = CalculateDiscount(p.PartnerProducts.Sum(pp => pp.Count))
                })
                .ToList();
        }

        private int CalculateDiscount(int totalSales)
        {
            if(totalSales > 300000)
            {
                return 15;
            } else if(totalSales > 50000)
            {
                return 10;
            } else if(totalSales > 10000)
            {
                return 5;
            }
            return 0;
        }
        
        public List<PartnerType> GetAllPartnerTypes()
        {
            return _context.PartnerTypes.ToList();
        }

        public void AddPartner(Partner partner)
        {
            try
            {
                _context.Partners.Add(partner);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка сохранения: " + ex.InnerException?.Message);
            }
        }

        public void UpdatePartner(Partner updatedPartner)
        {
            using (var context = new AppDbContext())
            {
                var existingPartner = context.Partners.Find(updatedPartner.PartnerId);
                context.Entry(existingPartner).CurrentValues.SetValues(updatedPartner);
                context.SaveChanges();
            }
        }
        
        public Partner GetPartnerById(int partnerId)
        {
            return _context.Partners
                .AsNoTracking() 
                .FirstOrDefault(p => p.PartnerId == partnerId);
        }
        
    }

}

