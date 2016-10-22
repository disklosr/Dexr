using Windows.UI.Xaml.Controls;

namespace Dex.Uwp.Pages
{
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            InitializeComponent();
        }

        public new Frame Frame => MainFrame;
    }
}