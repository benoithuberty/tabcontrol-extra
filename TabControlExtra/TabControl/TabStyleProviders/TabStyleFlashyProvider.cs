/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.Drawing;
using System.Windows.Forms;

namespace Adiict.UI.Forms
{
    public class TabStyleFlashyProvider : TabStyleProvider
    {
        private static readonly Color DefaultBaseColor = Color.FromArgb(0x4d, 0x96, 0xff);

        private Color _BaseColor = DefaultBaseColor;

        [System.ComponentModel.Category("Appearance"), System.ComponentModel.DefaultValue(typeof(Color), "77, 150, 255")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set
            {
                _BaseColor = value;
                ApplyColorScheme();
                TabControl.Invalidate();
            }
        }

        public TabStyleFlashyProvider(TabControlExtra tabControl) : base(tabControl)
        {
            Radius = 2;
            Padding = new Point(6, 5);
            ApplyColorScheme();
        }

        private void ApplyColorScheme()
        {
            Color b = _BaseColor;

            // Tab background gradients (solid — Color1 == Color2)
            TabColorSelected1    = b;
            TabColorSelected2    = b;
            TabColorFocused1     = Lighten(b, 0.15f);
            TabColorFocused2     = TabColorFocused1;
            TabColorHighlighted1 = Lighten(b, 0.30f);
            TabColorHighlighted2 = TabColorHighlighted1;
            TabColorUnselected1  = Darken(b, 0.45f);
            TabColorUnselected2  = TabColorUnselected1;
            TabColorDisabled1    = Desaturate(b);
            TabColorDisabled2    = TabColorDisabled1;

            // Page background colors mirror tab colors
            PageBackgroundColorSelected    = TabColorSelected1;
            PageBackgroundColorFocused     = TabColorFocused1;
            PageBackgroundColorHighlighted = TabColorHighlighted1;
            PageBackgroundColorUnselected  = TabColorUnselected1;
            PageBackgroundColorDisabled    = TabColorDisabled1;

            // Border colors
            BorderColorSelected    = Darken(b, 0.20f);
            BorderColorFocused     = b;
            BorderColorHighlighted = Darken(b, 0.10f);
            BorderColorUnselected  = Darken(b, 0.30f);
            BorderColorDisabled    = Desaturate(b);

            // Text colors
            TextColorSelected    = Color.White;
            TextColorFocused     = Color.White;
            TextColorHighlighted = Color.White;
            TextColorUnselected  = Lighten(b, 0.60f);
            TextColorDisabled    = Color.Gray;
        }

        private static Color Darken(Color c, float factor) =>
            Color.FromArgb(
                (int)(c.R * (1f - factor)),
                (int)(c.G * (1f - factor)),
                (int)(c.B * (1f - factor)));

        private static Color Lighten(Color c, float factor) =>
            Color.FromArgb(
                c.R + (int)((255 - c.R) * factor),
                c.G + (int)((255 - c.G) * factor),
                c.B + (int)((255 - c.B) * factor));

        private static Color Desaturate(Color c)
        {
            int luma = (c.R + c.G + c.B) / 3;
            return Color.FromArgb(luma, luma, luma);
        }
    }
}
