using Microsoft.Extensions.Options;
using SimpleCRM.Application.Repositories;
using SimpleCRM.Infrastructure.Database;
using System.Linq;
using System.Windows;

namespace SimpleCRM.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IOptions<DatabaseConfig> options, IInvoiceRepository invoiceRepository)
        {
            InitializeComponent();

            settingsTest.Content = options?.Value?.ConnectionStrings?.Default ?? "";
            dbTest.Content = invoiceRepository.GetAll().FirstOrDefault()?.Id?.ToString() ?? "";
        }
    }
}
