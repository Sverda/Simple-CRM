using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleCRM.Application.ViewModel;

namespace SimpleCRM.Application.Commands
{
    internal class ChangeMainContentNotification : INotification
    {
        public Type TargetContent { get; }
        public SidebarOptionViewModel SelectedOption { get; }

        public ChangeMainContentNotification(Type targetContent, SidebarOptionViewModel selectedOption)
        {
            bool isCore = targetContent.IsSubclassOf(typeof(CoreViewModel));
            if (!isCore)
            {
                throw new ArgumentException(
                    $"Type {targetContent.Name} should be of type {nameof(CoreViewModel)}.");
            }

            TargetContent = targetContent;
            SelectedOption = selectedOption;
        }
    }

    internal class ChangeMainContentNotificationHandler : INotificationHandler<ChangeMainContentNotification>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly LayoutViewModel layoutViewModel;
        private readonly SidebarViewModel sidebarViewModel;

        public ChangeMainContentNotificationHandler(
            IServiceProvider serviceProvider,
            LayoutViewModel layoutViewModel,
            SidebarViewModel sidebarViewModel)
        {
            this.serviceProvider = serviceProvider;
            this.layoutViewModel = layoutViewModel;
            this.sidebarViewModel = sidebarViewModel;
        }

        public Task Handle(ChangeMainContentNotification notification, CancellationToken cancellationToken)
        {
            sidebarViewModel.SelectOne(notification.SelectedOption);
            layoutViewModel.Content = (CoreViewModel)serviceProvider.GetRequiredService(notification.TargetContent);
            return Task.CompletedTask;
        }
    }
}
