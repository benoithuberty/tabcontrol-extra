using Adiict.UI.Forms;
using TabCtrl = Adiict.UI.Forms.TabControlExtra;

namespace TabControlExtra.Tests;

[TestClass]
public class TabStateTests
{
    [TestMethod]
    public void SelectedAndFocused_ReturnsFocused()
    {
        var state = TabCtrl.DetermineTabState(
            index: 0, selectedIndex: 0, containsFocus: true,
            tabEnabled: true, hotTrack: false, activeIndex: -1);

        Assert.AreEqual(TabState.Focused, state);
    }

    [TestMethod]
    public void SelectedAndNotFocused_ReturnsSelected()
    {
        var state = TabCtrl.DetermineTabState(
            index: 0, selectedIndex: 0, containsFocus: false,
            tabEnabled: true, hotTrack: false, activeIndex: -1);

        Assert.AreEqual(TabState.Selected, state);
    }

    [TestMethod]
    public void NotSelectedAndDisabled_ReturnsDisabled()
    {
        var state = TabCtrl.DetermineTabState(
            index: 1, selectedIndex: 0, containsFocus: false,
            tabEnabled: false, hotTrack: false, activeIndex: -1);

        Assert.AreEqual(TabState.Disabled, state);
    }

    [TestMethod]
    public void HotTrackOnActiveTab_ReturnsHighlighted()
    {
        var state = TabCtrl.DetermineTabState(
            index: 2, selectedIndex: 0, containsFocus: false,
            tabEnabled: true, hotTrack: true, activeIndex: 2);

        Assert.AreEqual(TabState.Highlighted, state);
    }

    [TestMethod]
    public void HotTrackOnDifferentTab_ReturnsUnselected()
    {
        var state = TabCtrl.DetermineTabState(
            index: 2, selectedIndex: 0, containsFocus: false,
            tabEnabled: true, hotTrack: true, activeIndex: 1);

        Assert.AreEqual(TabState.Unselected, state);
    }

    [TestMethod]
    public void NotSelectedEnabledHotTrackOff_ReturnsUnselected()
    {
        var state = TabCtrl.DetermineTabState(
            index: 1, selectedIndex: 0, containsFocus: false,
            tabEnabled: true, hotTrack: false, activeIndex: 1);

        Assert.AreEqual(TabState.Unselected, state);
    }

    [TestMethod]
    public void DisabledTabWithHotTrack_ReturnsDisabled()
    {
        var state = TabCtrl.DetermineTabState(
            index: 1, selectedIndex: 0, containsFocus: false,
            tabEnabled: false, hotTrack: true, activeIndex: 1);

        Assert.AreEqual(TabState.Disabled, state);
    }
}
