using MediatR;
using SimpleCRM.Application.ViewModel.Helpers;
using System.Collections.ObjectModel;

namespace SimpleCRM.Application.ViewModel
{
    public class SidebarViewModel : CoreViewModel
    {
        public ObservableCollection<SidebarOptionViewModel> Options { get; }

        public SidebarViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext) : base(mediator, serviceProvider, uiContext)
        {
            Options = new ObservableCollection<SidebarOptionViewModel>()
            {
                new SidebarOptionViewModel(
                    mediator,
                    serviceProvider,
                    uiContext,
                    "Dashboard",
                    typeof(DashboardViewModel),
                    true),
                new SidebarOptionViewModel(
                    mediator,
                    serviceProvider,
                    uiContext,
                    "Invoices",
                    typeof(InvoicesViewModel)),
                new SidebarOptionViewModel(
                    mediator,
                    serviceProvider,
                    uiContext,
                    "Settings",
                    typeof(SettingsViewModel)),
            };
        }

        public void SelectOne(SidebarOptionViewModel selectedOption)
        {
            foreach (var option in Options)
            {
                option.IsSelected = false;
            }

            selectedOption.IsSelected = true;
        }
    }
}
