using Microsoft.Extensions.Options;
using SimpleCRM.Infrastructure.Database;
using System.Windows;

namespace SimpleCRM.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IOptions<DatabaseConfig> options)
        {
            InitializeComponent();

            this.test.Content = options.Value.ConnectionStrings.Default;
        }
    }
}
