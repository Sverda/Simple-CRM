using MediatR;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public class DashboardViewModel : CoreViewModel
    {
        public DashboardViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext) : base(mediator, serviceProvider, uiContext)
        {
        }
    }
}
