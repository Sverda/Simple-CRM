using MediatR;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public class SettingsViewModel : CoreViewModel
    {
        public SettingsViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext) : base(mediator, serviceProvider, uiContext)
        {
        }
    }
}