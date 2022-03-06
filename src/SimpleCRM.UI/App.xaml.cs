using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Infrastructure;
using System;
using System.Windows;

namespace SimpleCRM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IConfiguration? Config { get; private set; }

        public IServiceProvider Services { get; }

        public App()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            ServiceCollection services = new();
            services.AddInfrastructure((IConfigurationRoot)Config);
            services.AddViews();
            Services = services.BuildServiceProvider();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
