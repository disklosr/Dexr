using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Infrastructure
{
    public class PageBase : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = ResolveViewModelForPage(e);
            viewModel.OnNavigatedTo(e);

            DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        private ViewModelBase ResolveViewModelForPage(NavigationEventArgs e)
        {
            var fullName = $"{e.SourcePageType.Namespace.Replace(".Pages", "")}.ViewModels.{e.SourcePageType.Name.Replace("Page", "ViewModel")}";
            Type viewModelType = Type.GetType(fullName);
            return (ViewModelBase)ServiceLocator.Current.GetInstance(viewModelType);
        }
    }
}