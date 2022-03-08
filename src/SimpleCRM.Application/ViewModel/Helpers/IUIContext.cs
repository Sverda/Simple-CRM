namespace SimpleCRM.Application.ViewModel.Helpers
{
    public interface IUIContext
    {
        bool IsSynchronized { get; }
        void Invoke(Action action);
        void BeginInvoke(Action action);
        void SetViewContext(object view, object context);
    }
}
