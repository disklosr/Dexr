using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Infrastructure
{
    public class PageBase : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = ResolveViewModelForPage();
            viewModel.OnNavigatedTo(e);

            DataContext = viewModel;

            base.OnNavigatedTo(e);
        }

        private ViewModelBase ResolveViewModelForPage()
        {
            throw new NotImplementedException();

            //string fullName = $"{e.SourcePageType.Namespace}.ViewModels.{e.SourcePageType.Name.Replace("Page", "ViewModel")}";
            //Type viewModelType = Type.GetType(fullName);
            //ViewModelBase viewModel = (ViewModelBase)ServiceLocator.Current.GetInstance(viewModelType);
        }
    }
}