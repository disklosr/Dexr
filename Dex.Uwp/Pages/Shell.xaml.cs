using Dex.Uwp.Infrastructure;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Dex.Uwp.Pages
{
    public sealed partial class Shell : Page
    {
        private PageBase currentPage;

        public Shell()
        {
            InitializeComponent();
            this.MainFrame.DataContextChanged += MainFrame_CurrentPageChanged;
        }

        public new Frame Frame => MainFrame;

        private void CurrentPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Page Title
            PageTitle.Text = currentPage.Title;

            //Set Application Bar Buttons
            commandsBar.Items.Clear();
            foreach (var item in currentPage.Commands)
            {
                commandsBar.Items.Add(item);
                item.DataContext = currentPage.DataContext;
            }

            currentPage.Loaded -= CurrentPage_Loaded;
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (RootSplitView.DisplayMode == SplitViewDisplayMode.CompactInline || RootSplitView.DisplayMode == SplitViewDisplayMode.Inline)
            {
                return;
            }

            RootSplitView.IsPaneOpen = false;
        }

        private void MainFrame_CurrentPageChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            currentPage = args.NewValue as PageBase;
            if (currentPage == null)
                return;
            currentPage.Loaded += CurrentPage_Loaded;
        }
    }
}