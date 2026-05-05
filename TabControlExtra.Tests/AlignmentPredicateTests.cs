using Adiict.UI.Forms;
using System.Drawing;
using TabCtrl = Adiict.UI.Forms.TabControlExtra;

namespace TabControlExtra.Tests;

[TestClass]
public class AlignmentPredicateTests
{
    [TestMethod]
    [DataRow(ContentAlignment.BottomLeft,   true)]
    [DataRow(ContentAlignment.MiddleLeft,   true)]
    [DataRow(ContentAlignment.TopLeft,      true)]
    [DataRow(ContentAlignment.BottomCenter, false)]
    [DataRow(ContentAlignment.MiddleCenter, false)]
    [DataRow(ContentAlignment.TopCenter,    false)]
    [DataRow(ContentAlignment.BottomRight,  false)]
    [DataRow(ContentAlignment.MiddleRight,  false)]
    [DataRow(ContentAlignment.TopRight,     false)]
    public void IsLeftAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsLeftAligned(alignment));

    [TestMethod]
    [DataRow(ContentAlignment.BottomRight,  true)]
    [DataRow(ContentAlignment.MiddleRight,  true)]
    [DataRow(ContentAlignment.TopRight,     true)]
    [DataRow(ContentAlignment.BottomCenter, false)]
    [DataRow(ContentAlignment.MiddleCenter, false)]
    [DataRow(ContentAlignment.TopCenter,    false)]
    [DataRow(ContentAlignment.BottomLeft,   false)]
    [DataRow(ContentAlignment.MiddleLeft,   false)]
    [DataRow(ContentAlignment.TopLeft,      false)]
    public void IsRightAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsRightAligned(alignment));

    [TestMethod]
    [DataRow(ContentAlignment.TopLeft,    true)]
    [DataRow(ContentAlignment.TopCenter,  true)]
    [DataRow(ContentAlignment.TopRight,   true)]
    [DataRow(ContentAlignment.MiddleLeft, false)]
    [DataRow(ContentAlignment.BottomLeft, false)]
    public void IsTopAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsTopAligned(alignment));

    [TestMethod]
    [DataRow(ContentAlignment.BottomLeft,   true)]
    [DataRow(ContentAlignment.BottomCenter, true)]
    [DataRow(ContentAlignment.BottomRight,  true)]
    [DataRow(ContentAlignment.MiddleLeft,   false)]
    [DataRow(ContentAlignment.TopLeft,      false)]
    public void IsBottomAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsBottomAligned(alignment));

    [TestMethod]
    [DataRow(ContentAlignment.MiddleLeft,   true)]
    [DataRow(ContentAlignment.MiddleCenter, true)]
    [DataRow(ContentAlignment.MiddleRight,  true)]
    [DataRow(ContentAlignment.TopLeft,      false)]
    [DataRow(ContentAlignment.BottomLeft,   false)]
    public void IsMiddleAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsMiddleAligned(alignment));

    [TestMethod]
    [DataRow(ContentAlignment.TopCenter,    true)]
    [DataRow(ContentAlignment.MiddleCenter, true)]
    [DataRow(ContentAlignment.BottomCenter, true)]
    [DataRow(ContentAlignment.TopLeft,      false)]
    [DataRow(ContentAlignment.MiddleRight,  false)]
    public void IsCenterAligned_ReturnsCorrectResult(ContentAlignment alignment, bool expected)
        => Assert.AreEqual(expected, TabCtrl.IsCenterAligned(alignment));
}
