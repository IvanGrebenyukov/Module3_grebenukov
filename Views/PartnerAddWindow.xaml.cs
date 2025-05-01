using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Models;

namespace WpfExamGrebenukov.Views;

public partial class PartnerAddWindow : Window
{
    private readonly AppDbContext _context;
    public PartnerAddWindow()
    {
        InitializeComponent();
        _context = new AppDbContext();
        LoadPartnerType();
    }

    private void LoadPartnerType()
    {
        var partnerTypes = _context.PartnerTypes.ToList();
        PartnersTypeComboBox.ItemsSource = partnerTypes;
    }
    
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (PartnersTypeComboBox.SelectedItem == null
            || string.IsNullOrWhiteSpace(innTextBox.Text)
            || string.IsNullOrWhiteSpace(titleTextBox.Text)
            || string.IsNullOrWhiteSpace(ratingTextBox.Text)
            || string.IsNullOrWhiteSpace(directorTextBox.Text)
            || string.IsNullOrWhiteSpace(addressTextBox.Text)
            || string.IsNullOrWhiteSpace(phoneTextBox.Text)
            || string.IsNullOrWhiteSpace(emailTextBox.Text))
        {
            MessageBox.Show("Заполните все поля", "Предупреждение", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(ratingTextBox.Text, out int rating) || rating < 0)
        {
            MessageBox.Show("Рейтинг должен быть целым положительным числом", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (!long.TryParse(innTextBox.Text, out long inn))
        {
            MessageBox.Show("ИНН должен быть числом", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            var newPartner = new Partner
            {
                Title = titleTextBox.Text,
                PartnerTypeId = (int)PartnersTypeComboBox.SelectedValue,
                Inn = long.Parse(innTextBox.Text),
                Rating = rating,
                AddressPartner = addressTextBox.Text,
                Director = directorTextBox.Text,
                PhonePartner = phoneTextBox.Text,
                EmailPartner = emailTextBox.Text
            };
            
            
            _context.Partners.Add(newPartner);
            _context.SaveChanges();

            MessageBox.Show("Новый партнет добавлен", "Успешно",
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
        catch (Exception ex)
        {
            var partnerData = new StringBuilder();
            partnerData.AppendLine("Данные партнера:");
            partnerData.AppendLine($"Title: {titleTextBox.Text}");
            partnerData.AppendLine($"PartnerTypeId: {PartnersTypeComboBox.SelectedValue}");
            partnerData.AppendLine($"INN: {innTextBox.Text}");
            partnerData.AppendLine($"Rating: {rating}");
            partnerData.AppendLine($"Address: {addressTextBox.Text}");
            partnerData.AppendLine($"Director: {directorTextBox.Text}");
            partnerData.AppendLine($"Phone: {phoneTextBox.Text}");
            partnerData.AppendLine($"Email: {emailTextBox.Text}");

            // Формируем сообщение об ошибке
            var errorMessage = new StringBuilder();
            errorMessage.AppendLine($"Ошибка: {ex.Message}");
    
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine();
                errorMessage.AppendLine($"Inner exception: {ex.InnerException.Message}");
            }

            // Выводим MessageBox с деталями
            MessageBox.Show(
                $"{errorMessage}\n\n{partnerData}", 
                "Ошибка добавления", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error
            );
            
            /*MessageBox.Show("Ошибка при добавлении данных", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);*/
        }
    }
}