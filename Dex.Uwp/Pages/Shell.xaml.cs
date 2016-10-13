using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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