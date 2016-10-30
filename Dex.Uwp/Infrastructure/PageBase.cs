using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace Dex.Uwp.Infrastructure
{
    public class CommandsCollection : List<ButtonBase> { }

    public class PageBase : Page
    {
        private static DependencyProperty CommandsProperty
            = DependencyProperty.Register("Commands", typeof(string), typeof(PageBase), new PropertyMetadata(new CommandsCollection()));

        private static DependencyProperty TitleProperty
           = DependencyProperty.Register("Title", typeof(string), typeof(PageBase), new PropertyMetadata("Dex"));

        private ViewModelBase viewModel;

        public CommandsCollection Commands
        {
            get { return (CommandsCollection)GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = viewModel ?? ResolveViewModelForPage(e);
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