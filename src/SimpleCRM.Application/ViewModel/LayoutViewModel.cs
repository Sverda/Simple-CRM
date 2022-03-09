using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public class LayoutViewModel : CoreViewModel
    {
        private CoreViewModel content;

        public CoreViewModel Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public LayoutViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext) : base(mediator, serviceProvider, uiContext)
        {
            Content = serviceProvider.GetRequiredService<DashboardViewModel>();
        }
    }
}
