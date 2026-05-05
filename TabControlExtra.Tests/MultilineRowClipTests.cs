using System.Drawing;
using System.Windows.Forms;
using Adiict.UI.Forms;
using TabCtrl = Adiict.UI.Forms.TabControlExtra;

namespace TabControlExtra.Tests;

[TestClass]
public class MultilineRowClipTests
{
    // ── Top-alignment helpers ────────────────────────────────────────────────
    // Two-row layout: tabs 0,1 in row 0 (Y=2..22), tab 2 in row 1 (Y=22..42).
    private static int TwoRowGetRow(int i) => i < 2 ? 0 : 1;
    private static Rectangle TwoRowGetRect(int i) =>
        i < 2
            ? new Rectangle(i * 80, 2, 78, 20)
            : new Rectangle(0, 22, 78, 20);

    [TestMethod]
    public void Top_Row0_NonSelectedTabs_ReturnsYBand()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 2, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(2,   bounds.Y);
        Assert.AreEqual(400, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }

    [TestMethod]
    public void Top_Row1_OnlySelectedTab_ReturnsEmpty()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 2, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(Rectangle.Empty, bounds);
    }

    [TestMethod]
    public void Top_Row1_NonSelectedTabPresent_ReturnsYBand()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 0, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: TwoRowGetRow, getTabRect: TwoRowGetRect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(22,  bounds.Y);
        Assert.AreEqual(400, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }

    [TestMethod]
    public void Top_MultipleTabsInRow_YBandSpansMinToMax()
    {
        static int row(int i) => i < 2 ? 0 : 1;
        static Rectangle rect(int i) => i == 0 ? new Rectangle(0,  2, 78, 20)
                                      : i == 1 ? new Rectangle(80, 4, 78, 18)
                                      :          new Rectangle(0, 22, 78, 20);

        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 2, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: row, getTabRect: rect);

        Assert.AreEqual(2,  bounds.Y);
        Assert.AreEqual(20, bounds.Height);  // maxBottom(22) - minY(2)
    }

    [TestMethod]
    public void Top_NoTabs_ReturnsEmpty()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: -1, tabCount: 0,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: _ => 0, getTabRect: _ => Rectangle.Empty);

        Assert.AreEqual(Rectangle.Empty, bounds);
    }

    [TestMethod]
    public void Top_SingleRow_AllNonSelectedTabsIncluded()
    {
        static int row(int i) => 0;
        static Rectangle rect(int i) => new Rectangle(i * 80, 2, 78, 20);

        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 1, tabCount: 3,
            clientWidth: 600, clientHeight: 300, alignment: TabAlignment.Top,
            getTabRow: row, getTabRect: rect);

        Assert.AreEqual(0,   bounds.X);
        Assert.AreEqual(2,   bounds.Y);
        Assert.AreEqual(600, bounds.Width);
        Assert.AreEqual(20,  bounds.Height);
    }

    // ── Left-alignment helpers ───────────────────────────────────────────────
    // Two-column layout: tabs 0,1 in col 0 (X=2..22), tab 2 in col 1 (X=22..42).
    private static int TwoColGetRow(int i) => i < 2 ? 0 : 1;
    private static Rectangle TwoColGetRect(int i) =>
        i < 2
            ? new Rectangle(2,  i * 80, 20, 78)   // col 0, different Y positions
            : new Rectangle(22, 0,      20, 78);   // col 1

    [TestMethod]
    public void Left_Col0_NonSelectedTabs_ReturnsXBand()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 0, selectedIndex: 2, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Left,
            getTabRow: TwoColGetRow, getTabRect: TwoColGetRect);

        Assert.AreEqual(2,   bounds.X);
        Assert.AreEqual(0,   bounds.Y);
        Assert.AreEqual(20,  bounds.Width);
        Assert.AreEqual(300, bounds.Height);
    }

    [TestMethod]
    public void Left_Col1_OnlySelectedTab_ReturnsEmpty()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 2, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Left,
            getTabRow: TwoColGetRow, getTabRect: TwoColGetRect);

        Assert.AreEqual(Rectangle.Empty, bounds);
    }

    [TestMethod]
    public void Left_Col1_NonSelectedTabPresent_ReturnsXBand()
    {
        Rectangle bounds = TabCtrl.ComputeRowClipBounds(
            row: 1, selectedIndex: 0, tabCount: 3,
            clientWidth: 400, clientHeight: 300, alignment: TabAlignment.Left,
            getTabRow: TwoColGetRow, getTabRect: TwoColGetRect);

        Assert.AreEqual(22,  bounds.X);
        Assert.AreEqual(0,   bounds.Y);
        Assert.AreEqual(20,  bounds.Width);
        Assert.AreEqual(300, bounds.Height);
    }
}
