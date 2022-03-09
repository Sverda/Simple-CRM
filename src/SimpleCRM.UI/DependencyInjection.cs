using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.ViewModel.Helpers;
using SimpleCRM.UI.View;
using SimpleCRM.UI.View.Helpers;

namespace SimpleCRM.UI
{
    public static class DependencyInjection
    {
        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<IUIContext, WPFContext>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<LayoutView>();
        }
    }
}
