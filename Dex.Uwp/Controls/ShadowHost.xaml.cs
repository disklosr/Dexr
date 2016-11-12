using Dex.Uwp.Theme;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Dex.Uwp.Controls
{
    public sealed partial class ShadowHost : ContentControl
    {
        private const float blurRadius = 4.0f;
        private readonly Color shadowColor = Color.FromArgb(96, 0, 0, 0);
        private readonly Vector3 shadowOffset = new Vector3(0f, 2f, 2f);

        public ShadowHost()
        {
            this.InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            var shadowHost = (Canvas)GetTemplateChild("ShadowCanvas");

            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                InitializeDropShadow(shadowHost);

            base.OnApplyTemplate();
        }

        private void InitializeDropShadow(UIElement shadowHost)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);
            Compositor compositor = hostVisual.Compositor;

            // Create a drop shadow
            var dropShadow = compositor.CreateDropShadow();

            dropShadow.Color = shadowColor;
            dropShadow.BlurRadius = blurRadius;
            dropShadow.Offset = shadowOffset;

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