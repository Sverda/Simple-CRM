using MediatR;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public class InvoicesViewModel : CoreViewModel
    {
        public InvoicesViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext) : base(mediator, serviceProvider, uiContext)
        {
        }
    }
}
