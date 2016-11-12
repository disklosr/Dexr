using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Dex.Uwp.Controls
{
    public sealed partial class RectangularGauge : UserControl
    {
        public RectangularGauge()
        {
            this.InitializeComponent();
            InitAnimations();
        }

        private void InitAnimations()
        {
            //TODO: This is not working!
            var visual = ElementCompositionPreview.GetElementVisual(ValueRectangle);
            var compositor = visual.Compositor;

            var sizeAnimation = compositor.CreateVector2KeyFrameAnimation();
            sizeAnimation.Target = "Size";
            sizeAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            sizeAnimation.Duration = TimeSpan.FromMilliseconds(1500);

            var implicitAnimations = compositor.CreateImplicitAnimationCollection();
            visual.ImplicitAnimations = implicitAnimations;

            implicitAnimations["Size"] = sizeAnimation;
        }
    }
}