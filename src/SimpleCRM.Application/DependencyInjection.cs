using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.ViewModel;

namespace SimpleCRM.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this ServiceCollection services)
        {
            services.AddMediatR(typeof(CoreViewModel));

            services.AddSingleton<LayoutViewModel>();
            services.AddSingleton<SidebarViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<InvoicesViewModel>();
            services.AddTransient<SettingsViewModel>();
        }
    }
}
