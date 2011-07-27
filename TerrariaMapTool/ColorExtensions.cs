using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TerrariaMapTool {
    public static class ColorExtensions {
        public static Color Lerp(this Color first, Color second, float blend) {
            return Color.FromArgb(
                (int) (first.R + blend * (second.R - first.R)),
                (int) (first.G + blend * (second.G - first.G)),
                (int) (first.B + blend * (second.B - first.B)));
        }
    }
}
