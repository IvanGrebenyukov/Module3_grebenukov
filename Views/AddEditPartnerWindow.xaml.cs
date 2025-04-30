using System.Windows;
using System.Windows.Input;
using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Models;
using WpfExamGrebenukov.Services;

namespace WpfExamGrebenukov.Views;

public partial class AddEditPartnerWindow : Window
{
    
    private readonly PartnerService _partnerService;
        
    public string WindowTitle { get; }
    public Partner CurrentPartner { get; set; }
    public List<PartnerType> PartnerTypes { get; }
        
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand ValidateRatingCommand { get; }
    public ICommand ValidateEmailCommand { get; }
    
    public AddEditPartnerWindow(Partner partner, PartnerService partnerService)
    {
        InitializeComponent();
            
        _partnerService = partnerService;
         
        PartnerTypes = _partnerService.GetAllPartnerTypes();
    
        
        CurrentPartner = new Partner();
        
        if (partner != null)
        {
            CurrentPartner.PartnerId = partner.PartnerId;
            CurrentPartner.Title = partner.Title;
            CurrentPartner.PartnerTypeId = partner.PartnerTypeId;
            CurrentPartner.Rating = partner.Rating;
            CurrentPartner.AddressPartner = partner.AddressPartner;
            CurrentPartner.Director = partner.Director;
            CurrentPartner.PhonePartner = partner.PhonePartner;
            CurrentPartner.EmailPartner = partner.EmailPartner;
        }
        
        // Загружаем типы партнеров из БД
        using (var context = new AppDbContext())
        {
            PartnerTypes = context.PartnerTypes.ToList();
        }
            
        // Инициализируем текущего партнера
        CurrentPartner = partner ?? new Partner();
        WindowTitle = partner == null ? "Добавление партнера" : "Редактирование партнера";
            
        // Команды
        SaveCommand = new RelayCommand(_ => SavePartner(), _ => CanSave());
        CancelCommand = new RelayCommand(_ => DialogResult = false);
        ValidateRatingCommand = new RelayCommand(_ => ValidateRating());
        ValidateEmailCommand = new RelayCommand(_ => ValidateEmail());
            
        DataContext = this;
    }
    
    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(CurrentPartner.Title) &&
               CurrentPartner.PartnerTypeId > 0 &&
               CurrentPartner.Rating >= 0 &&
               !string.IsNullOrWhiteSpace(CurrentPartner.AddressPartner) &&
               !string.IsNullOrWhiteSpace(CurrentPartner.Director) &&
               !string.IsNullOrWhiteSpace(CurrentPartner.PhonePartner) &&
               !string.IsNullOrWhiteSpace(CurrentPartner.EmailPartner);
    }

    private void SavePartner()
    {
        try
        {
            if (CurrentPartner.PartnerId == 0)
            {
                _partnerService.AddPartner(CurrentPartner);
            }
            else
            {
                _partnerService.UpdatePartner(CurrentPartner);
            }
                
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ValidateRating()
    {
        if (CurrentPartner.Rating < 0)
        {
            MessageBox.Show("Рейтинг должен быть положительным числом", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            CurrentPartner.Rating = 0;
        }
    }

    private void ValidateEmail()
    {
        if (!CurrentPartner.EmailPartner.Contains("@"))
        {
            MessageBox.Show("Введите корректный email адрес", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
}