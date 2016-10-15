using System.Numerics;
using Windows.UI;

namespace Dex.Uwp.Theme
{
    public class DropShadowStyle
    {
        public readonly Color Color = Color.FromArgb(120, 0, 0, 0);
        public readonly Vector3 Offset = new Vector3(0f, 2f, 2f);
        public float BlurRadius = 4.0f;
    }
}