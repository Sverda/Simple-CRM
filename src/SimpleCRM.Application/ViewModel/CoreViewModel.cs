using MediatR;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public abstract class CoreViewModel : ObservableObject
    {
        protected readonly IMediator mediator;
        protected readonly IUIContext uiContext;
        protected readonly IServiceProvider serviceProvider;

        public CoreViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext)
        {
            this.mediator = mediator;
            this.serviceProvider = serviceProvider;
            this.uiContext = uiContext;
        }
    }
}
