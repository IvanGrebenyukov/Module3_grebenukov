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

using WpfExamGrebenukov.Data;
using WpfExamGrebenukov.Models;
using WpfExamGrebenukov.Services;
using WpfExamGrebenukov.Views;

namespace WpfExamGrebenukov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly PartnerService _partnerService;
        private readonly AppDbContext _context = new AppDbContext();
        
         private ObservableCollection<Partner> _partners;
        
            public ObservableCollection<Partner> Partners
            {
                get => _partners;
                set
                {
                    _partners = value;
                    OnPropertyChanged();
                }
            }
        
        public ICommand AddPartnerCommand { get; }
        public ICommand EditPartnerCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var context = new AppDbContext();
            _partnerService = new PartnerService(_context);
        
            AddPartnerCommand = new RelayCommand(_ => ShowAddEditPartnerWindow());
            EditPartnerCommand = new RelayCommand(ExecuteEditCommand);
        
            LoadPartners();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        private void LoadPartners()
        {
            try
            {
                var partners = _partnerService.GetPartnersWithDiscount();
                PartnersItemsControl.ItemsSource = partners;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка загрузки данных", ex.Message);
            }
        }

        private void ShowAddEditPartnerWindow(object parameter = null)
        {
            try
            {
                Partner originalPartner = null;
                if (parameter is Partner selectedPartner)
                {
                    // Получаем свежую копию из базы
                    originalPartner = _partnerService.GetPartnerById(selectedPartner.PartnerId);
                }

                var window = new AddEditPartnerWindow(originalPartner, _partnerService);
        
                if (window.ShowDialog() == true)
                {
                    LoadPartners();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка", ex.Message);
            }
        }

        private void ShowErrorMessage(string title, string message)
        {
            var dialog = new MaterialDesignThemes.Wpf.DialogHost {
                DialogContent = new StackPanel {
                    Children = {
                        new TextBlock { Text = title, FontWeight = FontWeights.Bold },
                        new TextBlock { Text = message, Margin = new Thickness(0,10,0,0) }
                    }
                }
            };
            
            MaterialDesignThemes.Wpf.DialogHost.Show(dialog, "RootDialog");
        }
        
        private void ExecuteEditCommand(object parameter)
        {
            if (parameter is Partner partner)
            {
                ShowAddEditPartnerWindow(partner);
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}