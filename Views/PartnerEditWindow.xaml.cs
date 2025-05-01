using System.Windows;
using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Dtos;

namespace WpfExamGrebenukov.Views;

public partial class PartnerEditWindow : Window
{
    private readonly AppDbContext _context;
    
    public PartnerEditWindow(PartnerWithDiscountDto partner)
    {
        InitializeComponent();
        _context = new AppDbContext();
        LoadPartnerType();
        DataContext = partner;
    }

    private void LoadPartnerType()
    {
        var partnerType = _context.PartnerTypes.ToList();
        PartnersTypeComboBox.ItemsSource = partnerType;
    }
    
    
    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (!(DataContext is PartnerWithDiscountDto partner))
        {
            MessageBox.Show("Не удалось загрузить данные партнера", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            var editPartner = _context.Partners.FirstOrDefault(p => p.PartnerId == partner.PartnerId);

            editPartner.Title = titleTextBox.Text;
            editPartner.PartnerTypeId = (int)PartnersTypeComboBox.SelectedValue;
            editPartner.Inn = long.Parse(innTextBox.Text);
            editPartner.Rating = int.Parse(ratingTextBox.Text);
            editPartner.AddressPartner = addressTextBox.Text;
            editPartner.Director = directorTextBox.Text;
            editPartner.PhonePartner = phoneTextBox.Text;
            editPartner.EmailPartner = emailTextBox.Text;
            
            _context.SaveChanges();
            
            MessageBox.Show("Информация о партнере изменена", "Успешно",
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
        catch
        {
            MessageBox.Show("Ошибка при сохранении данных", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}