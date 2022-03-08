using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SimpleCRM.UI.View.Helpers
{
    public static class ViewModelLocator
    {
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel",
                                                 typeof(bool), typeof(ViewModelLocator),
                                                 new PropertyMetadata(false, OnAutoWireViewModelChanged));

        private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var viewType = d.GetType().Name;
            var viewModelTypeName = viewType + "Model";
            var assembly = typeof(Application.DependencyInjection).Assembly;
            var viewModelType = assembly.GetTypes().FirstOrDefault(t => t.FullName.Contains(viewModelTypeName));
            if (viewModelType is null)
            {
                return;
            }

            var viewModel = App.Current.Services.GetService(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
