using SimpleCRM.Application.ViewModel;
using SimpleCRM.Application.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCRM.UI.View.Helpers
{
    internal class ViewLocator : IViewLocator
    {
        private readonly Dictionary<Type, Type> viewModelViewMapping;

        public ViewLocator()
        {
            viewModelViewMapping = new Dictionary<Type, Type>();
            PopulateWithMappings();
        }

        private void PopulateWithMappings()
        {
            var viewModels = typeof(CoreViewModel).Assembly
                            .GetTypes()
                            .Where(t => t.FullName.EndsWith("ViewModel") && !t.FullName.Contains(nameof(CoreViewModel)));
            foreach (var vm in viewModels)
            {
                var view = typeof(ViewLocator).Assembly
                    .GetTypes()
                    .Single(t => t.FullName.Contains(vm.Name.Replace("ViewModel", "View")));
                viewModelViewMapping.Add(vm, view);
            }
        }

        public Type? GetViewForViewModel(Type viewModel)
        {
            return viewModelViewMapping.ContainsKey(viewModel) ? viewModelViewMapping[viewModel] : null;
        }
    }
}
