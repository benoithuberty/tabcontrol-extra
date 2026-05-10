/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System.Drawing;
using System.Windows.Forms;

namespace Adiict.UI.Forms
{
    public class TabStyleThemeAwareProvider : TabStyleProvider
    {
        public TabStyleThemeAwareProvider(TabControlExtra tabControl) : base(tabControl)
        {
            Radius = 2;
            Padding = new Point(6, 4);

            // Tab background colors — SystemColors are KnownColor-backed and resolve dynamically at draw time
            TabColorSelected1    = SystemColors.Highlight;
            TabColorSelected2    = SystemColors.Highlight;
            TabColorFocused1     = SystemColors.ActiveCaption;
            TabColorFocused2     = SystemColors.ActiveCaption;
            TabColorHighlighted1 = SystemColors.ControlLight;
            TabColorHighlighted2 = SystemColors.ControlLight;
            TabColorUnselected1  = SystemColors.Control;
            TabColorUnselected2  = SystemColors.Control;
            TabColorDisabled1    = SystemColors.ControlLight;
            TabColorDisabled2    = SystemColors.ControlLight;

            // Page background colors mirror tab colors
            PageBackgroundColorSelected    = SystemColors.Highlight;
            PageBackgroundColorFocused     = SystemColors.ActiveCaption;
            PageBackgroundColorHighlighted = SystemColors.ControlLight;
            PageBackgroundColorUnselected  = SystemColors.Control;
            PageBackgroundColorDisabled    = SystemColors.ControlLight;

            // Border colors
            BorderColorSelected    = SystemColors.Highlight;
            BorderColorFocused     = SystemColors.ActiveCaption;
            BorderColorHighlighted = SystemColors.ControlDark;
            BorderColorUnselected  = SystemColors.ControlDark;
            BorderColorDisabled    = SystemColors.ControlLight;

            // Text colors
            TextColorSelected    = SystemColors.HighlightText;
            TextColorFocused     = SystemColors.ActiveCaptionText;
            TextColorHighlighted = SystemColors.ControlText;
            TextColorUnselected  = SystemColors.ControlText;
            TextColorDisabled    = SystemColors.GrayText;
        }
    }
}
