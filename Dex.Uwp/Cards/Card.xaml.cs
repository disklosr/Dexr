using Dex.Uwp.Theme;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Dex.Uwp.Cards
{
    public sealed partial class Card : ContentControl
    {
        public Card()
        {
            this.InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            var source = (Grid)GetTemplateChild("CardRoot");
            var target = (Canvas)GetTemplateChild("ShadowHost");

            InitializeDropShadow(source, target);

            base.OnApplyTemplate();
        }

        private void InitializeDropShadow(UIElement shadowShape, UIElement shadowHost)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(shadowShape);
            Compositor compositor = hostVisual.Compositor;

            // Create a drop shadow
            var dropShadow = compositor.CreateDropShadow();

            var shadowStyle = (DropShadowStyle)Application.Current.Resources["DropShadowStyle"];
            dropShadow.Color = shadowStyle.Color;
            dropShadow.BlurRadius = shadowStyle.BlurRadius;
            //dropShadow.Offset = shadowStyle.Offset;

            // Create a Visual to hold the shadow
            var shadowVisual = compositor.CreateSpriteVisual();
            shadowVisual.Shadow = dropShadow;

            // Add the shadow as a child of the host in the visual tree
            ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

            // Make sure size of shadow host and shadow visual always stay in sync
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

            shadowVisual.StartAnimation("Size", bindSizeAnimation);
        }
    }
}