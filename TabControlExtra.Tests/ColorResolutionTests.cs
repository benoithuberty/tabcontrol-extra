using Adiict.UI.Forms;
using System.Drawing;
using System.Windows.Forms;
using TabCtrl = Adiict.UI.Forms.TabControlExtra;

namespace TabControlExtra.Tests;

/// <summary>
/// Tests for TabStyleProvider color resolution and GetTabBackgroundColors.
/// Uses TabStyleNoneProvider (simplest concrete subclass) to exercise base-class logic.
/// </summary>
[TestClass]
public class ColorResolutionTests
{
    private static TabStyleProvider CreateProvider()
    {
        var control = new TabCtrl();
        control.DisplayStyle = TabStyle.None;
        return control.DisplayStyleProvider;
    }

    // --- TextColorDisabled bug regression ---

    [TestMethod]
    public void TextColorDisabled_DefaultsToControlDark_WhenNeitherIsSet()
    {
        var provider = CreateProvider();
        Assert.AreEqual(SystemColors.ControlDark, provider.TextColorDisabled);
    }

    [TestMethod]
    public void TextColorDisabled_ReturnsCustomValue_WhenSet()
    {
        var provider = CreateProvider();
        provider.TextColorDisabled = Color.Red;
        Assert.AreEqual(Color.Red, provider.TextColorDisabled);
    }

    [TestMethod]
    public void TextColorDisabled_IsIndependentOfTextColorUnselected()
    {
        var provider = CreateProvider();
        // Setting TextColorUnselected must NOT affect TextColorDisabled (was a bug).
        provider.TextColorUnselected = Color.Blue;
        Assert.AreEqual(SystemColors.ControlDark, provider.TextColorDisabled);
    }

    // --- GetTabBackgroundColors ---

    [TestMethod]
    public void GetTabBackgroundColors_Disabled_ReturnsDisabledColors()
    {
        var provider = CreateProvider();
        provider.TabColorDisabled1 = Color.Gray;
        provider.TabColorDisabled2 = Color.DarkGray;

        var (c1, c2) = provider.GetTabBackgroundColors(TabState.Disabled);

        Assert.AreEqual(Color.Gray, c1);
        Assert.AreEqual(Color.DarkGray, c2);
    }

    [TestMethod]
    public void GetTabBackgroundColors_Focused_ReturnsFocusedColors()
    {
        var provider = CreateProvider();
        provider.TabColorFocused1 = Color.Blue;
        provider.TabColorFocused2 = Color.LightBlue;

        var (c1, c2) = provider.GetTabBackgroundColors(TabState.Focused);

        Assert.AreEqual(Color.Blue, c1);
        Assert.AreEqual(Color.LightBlue, c2);
    }

    [TestMethod]
    public void GetTabBackgroundColors_Highlighted_ReturnsHighlightedColors()
    {
        var provider = CreateProvider();
        provider.TabColorHighlighted1 = Color.Yellow;
        provider.TabColorHighlighted2 = Color.LightYellow;

        var (c1, c2) = provider.GetTabBackgroundColors(TabState.Highlighted);

        Assert.AreEqual(Color.Yellow, c1);
        Assert.AreEqual(Color.LightYellow, c2);
    }

    [TestMethod]
    public void GetTabBackgroundColors_Selected_ReturnsSelectedColors()
    {
        var provider = CreateProvider();
        provider.TabColorSelected1 = Color.Green;
        provider.TabColorSelected2 = Color.LightGreen;

        var (c1, c2) = provider.GetTabBackgroundColors(TabState.Selected);

        Assert.AreEqual(Color.Green, c1);
        Assert.AreEqual(Color.LightGreen, c2);
    }

    [TestMethod]
    public void GetTabBackgroundColors_Unselected_ReturnsUnselectedColors()
    {
        var provider = CreateProvider();
        provider.TabColorUnselected1 = Color.White;
        provider.TabColorUnselected2 = Color.WhiteSmoke;

        var (c1, c2) = provider.GetTabBackgroundColors(TabState.Unselected);

        Assert.AreEqual(Color.White, c1);
        Assert.AreEqual(Color.WhiteSmoke, c2);
    }

    // --- Opacity validation ---

    [TestMethod]
    public void Opacity_ThrowsOnNegativeValue()
    {
        var provider = CreateProvider();
        bool threw = false;
        try { provider.Opacity = -0.1f; }
        catch (ArgumentException) { threw = true; }
        Assert.IsTrue(threw, "Expected ArgumentException for negative opacity");
    }

    [TestMethod]
    public void Opacity_ThrowsOnValueGreaterThanOne()
    {
        var provider = CreateProvider();
        bool threw = false;
        try { provider.Opacity = 1.1f; }
        catch (ArgumentException) { threw = true; }
        Assert.IsTrue(threw, "Expected ArgumentException for opacity > 1");
    }

    [TestMethod]
    public void Opacity_AcceptsBoundaryValues()
    {
        var provider = CreateProvider();
        provider.Opacity = 0f;
        Assert.AreEqual(0f, provider.Opacity);
        provider.Opacity = 1f;
        Assert.AreEqual(1f, provider.Opacity);
    }
}
