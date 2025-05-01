using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Dtos;
using WpfExamGrebenukov.Models;
using WpfExamGrebenukov.Views;

namespace WpfExamGrebenukov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        public ObservableCollection<PartnerWithDiscountDto> Partners { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            Partners = new ObservableCollection<PartnerWithDiscountDto>();
            DataContext = this;

            LoadPartners();
        }

        private void LoadPartners()
        {
            var partners = _context.Partners
                .Include(p => p.PartnerTypes)
                .Include(p => p.PartnerProducts)
                .ToList();

            foreach (var partner in partners)
            {
                int totalsales = partner.PartnerProducts.Sum(pp => pp.Count);
                int discount = CalculateDiscount(totalsales);
                
                Partners.Add(new PartnerWithDiscountDto(partner)
                {
                        TotalSales = totalsales,
                        Discount = discount
                });
            }
        }

        private int CalculateDiscount(int totalsales)
        {
            if (totalsales >= 300000) return 15;
            if(totalsales >= 50000) return 10;
            if(totalsales >= 10000) return 5;
            return 0;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _context.Dispose();
            base.OnClosing(e);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var partnerAddWindow = new PartnerAddWindow();
            if (partnerAddWindow.ShowDialog() == true)
            {
                Partners.Clear();
                LoadPartners();
            }
        }
        

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PartnerWithDiscountDto selectedPartner)
            {
                var partnerEditWindow = new PartnerEditWindow(selectedPartner);
                if(partnerEditWindow.ShowDialog() == true)
                {
                    Partners.Clear();
                    LoadPartners();
                }
            }
        }
    }
}