using System.Drawing;
using Adiict.UI.Forms;
using TabCtrl = Adiict.UI.Forms.TabControlExtra;

namespace TabControlExtra.Tests;

[TestClass]
public class MultilineRowClipTests
{
    // Two-row layout: tabs 0,1 in row 0 (Y=2..22), tab 2 in row 1 (Y=22..42).
    private static int TwoRowGetRow(int i) => i < 2 ? 0 : 1;
    private static Rectangle TwoRowGetRect(int i) =>
        i < 2
            ? new Rectangle(i * 80, 2, 78, 20)
            : new Rectangle(0, 22, 78, 20);

    [TestMethod]
    public void Row0_NonSelectedTabs_ReturnsTheirUnionBounds()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 2, tabCount: 3, clientWidth: 400,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(2,   bounds.Y);
        Assert.AreEqual(400, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }

    [TestMethod]
    public void Row1_OnlySelectedTab_ReturnsEmpty()
    {
        // Tab 2 is in row 1 and is also the selected tab — no non-selected tabs in row 1.
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 2, tabCount: 3, clientWidth: 400,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(Rectangle.Empty, bounds);
    }

    [TestMethod]
    public void Row1_NonSelectedTabPresent_ReturnsThatTabBounds()
    {
        // Selected = tab 0 (row 0). Tab 2 (row 1) is non-selected.
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 0, tabCount: 3, clientWidth: 400,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(22,  bounds.Y);
        Assert.AreEqual(400, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }

    [TestMethod]
    public void MultipleTabsInRow_BoundsSpanMinYToMaxBottom()
    {
        // Tabs in row 0 at slightly different vertical positions — union must span both.
        static int row(int i) => i < 2 ? 0 : 1;
        static Rectangle rect(int i) => i == 0 ? new Rectangle(0,  2, 78, 20)   // Y=2,  Bottom=22
                                      : i == 1 ? new Rectangle(80, 4, 78, 18)   // Y=4,  Bottom=22
                                      :          new Rectangle(0, 22, 78, 20);

        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 2, tabCount: 3, clientWidth: 400,
            getTabRow: row, getTabRect: rect);

        Assert.AreEqual(2,  bounds.Y);
        Assert.AreEqual(20, bounds.Height);  // max Bottom (22) - min Y (2)
    }

    [TestMethod]
    public void NoTabs_ReturnsEmpty()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: -1, tabCount: 0, clientWidth: 400,
            getTabRow: _ => 0, getTabRect: _ => Rectangle.Empty);

        Assert.AreEqual(Rectangle.Empty, bounds);
    }

    [TestMethod]
    public void SingleRow_AllNonSelectedTabsIncluded()
    {
        // RowCount==1 scenario: all tabs share row 0; only the selected one is excluded.
        static int row(int i) => 0;
        static Rectangle rect(int i) => new Rectangle(i * 80, 2, 78, 20);

        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 1, tabCount: 3, clientWidth: 600,
            getTabRow: row, getTabRect: rect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(2,   bounds.Y);
        Assert.AreEqual(600, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }
}
