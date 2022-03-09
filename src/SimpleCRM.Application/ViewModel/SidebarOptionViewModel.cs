using MediatR;
using Microsoft.Toolkit.Mvvm.Input;
using SimpleCRM.Application.Commands;
using SimpleCRM.Application.ViewModel.Helpers;

namespace SimpleCRM.Application.ViewModel
{
    public class SidebarOptionViewModel : CoreViewModel
    {
        private readonly Type targetContent;
        private string optionName;
        private bool isSelected;

        public string OptionName
        {
            get => optionName;
            set
            {
                optionName = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public IAsyncRelayCommand ClickedCommand { get; }

        public SidebarOptionViewModel(
            IMediator mediator,
            IServiceProvider serviceProvider,
            IUIContext uiContext,
            string optionName,
            Type targetContent,
            bool isSelected = false) : base(mediator, serviceProvider, uiContext)
        {
            ArgumentNullException.ThrowIfNull(optionName, nameof(optionName));
            OptionName = optionName;
            this.targetContent = targetContent;
            IsSelected = isSelected;
            ClickedCommand = new AsyncRelayCommand(ChangeMainContent);
        }

        private async Task ChangeMainContent()
        {
            await mediator.Publish(new ChangeMainContentNotification(targetContent, this));
        }
    }
}
