using Microsoft.Extensions.DependencyInjection;

namespace SimpleCRM.UI
{
    public static class DependencyInjection
    {
        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
        }
    }
}
