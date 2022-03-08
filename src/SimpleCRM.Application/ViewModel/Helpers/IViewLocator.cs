namespace SimpleCRM.Application.ViewModel.Helpers
{
    public interface IViewLocator
    {
        Type? GetViewForViewModel(Type viewModel);
    }
}
