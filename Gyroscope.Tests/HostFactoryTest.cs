using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gyroscope.Tests
{
    using FluentAssertions;

    [TestClass]
    public class HostFactoryTest
    {
        HostFactory tenXtenGrid = new HostFactory(1, 1, 1, 1, 1, 10, 10);

        [TestMethod]
        public void SmallGrid()
        {
            // 10 rows, 10 columns on a 0.001x0.001 host
            Grid grid = tenXtenGrid.CreateHost(0.001f, 0.001f);

            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            // 0.001f / 10 is 0.000100000005f in float arithmetic, so compare with a tolerance.
            Assert.AreEqual(0.0001f, grid.CellSize.Width, 1e-7f);
            Assert.AreEqual(0.0001f, grid.CellSize.Length, 1e-7f);
            Assert.AreEqual(grid.CellCount, 100);
        }

        [TestMethod]
        public void LargeGrid()
        {
            // 10 rows, 10 columns on a 100x100 host
            Grid grid = tenXtenGrid.CreateHost(100, 100);

            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            Assert.AreEqual(grid.CellSize.Width, 10);
            Assert.AreEqual(grid.CellSize.Length, 10);
            Assert.AreEqual(grid.CellCount, 100);
        }

        [TestMethod]
        public void LargeFloatingPointGrid()
        {
            // 10 rows, 10 columns on a 100.5x100.5 host
            Grid grid = tenXtenGrid.CreateHost(100.5f, 100.5f);

            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            Assert.AreEqual(grid.CellSize.Width, 10.05f);
            Assert.AreEqual(grid.CellSize.Length, 10.05f);
            Assert.AreEqual(grid.CellCount, 100);
        }

        [TestMethod]
        public void UnevenGrid()
        {
            // 10 rows, 10 columns on a 0.001x100 host
            Grid grid = this.tenXtenGrid.CreateHost(0.001f, 100f);

            (0.001 / 10).Should().BeApproximately(0.0001, 1e-12, "because 0.001 / 10 is approximately 0.0001 in double precision");
            (100 / 10).Should().Be(10, "because 100 / 10 is exactly 10");

            // Float division of 0.001 / 10 yields 0.000100000005, not exactly 0.0001 — the
            // original assumption that floats incur no rounding error here is incorrect.
            (0.001f / 10f).Should().BeApproximately(0.0001f, 1e-7f, "because 0.001f / 10f is only approximately 0.0001f using floats");
            (100f / 10f).Should().Be(10f, "because 100f / 10f is exactly 10f using floats");

            grid.RowCount.Should().Be(10, "because we are making a 10x10 grid");
            grid.ColumnCount.Should().Be(10, "because we are making a 10x10 grid");
            grid.CellSize.Width.Should().BeApproximately(0.0001f, 1e-7f, "because a width of 0.001f divided by 10 columns is approximately 0.0001f");
            grid.CellSize.Length.Should().Be(10f, "because a length of 100f divided by 10 rows in the grid is 10");
            grid.CellCount.Should().Be(100, "because a 10x10 grid is a simple multiplication resulting in 100");
        }

        [TestMethod]
        public void RectangleLargeGrid()
        {
            // 10 rows, 10 columns on a 100x10 host
            Grid grid = tenXtenGrid.CreateHost(100, 10);

            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            Assert.AreEqual(grid.CellSize.Width, 10);
            Assert.AreEqual(grid.CellSize.Length, 1);
            Assert.AreEqual(grid.CellCount, 100);
        }

        [TestMethod]
        public void RectangleSmallGrid()
        {
            // 10 rows, 10 columns on a 0.001x0.01 host
            Grid grid = tenXtenGrid.CreateHost(0.001f, 0.01f);

            // Cell should be 55
            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            // Float division leaves tiny rounding error, so compare with a tolerance.
            Assert.AreEqual(0.0001f, grid.CellSize.Width, 1e-7f);
            Assert.AreEqual(0.001f, grid.CellSize.Length, 1e-7f);
            Assert.AreEqual(grid.CellCount, 100);
        }

        [TestMethod]
        public void RectangleLargeFloatingPointGrid()
        {
            // 10 rows, 10 columns on a 100.5x10.5 host
            Grid grid = tenXtenGrid.CreateHost(100.5f, 10.5f);

            // Cell should be 55
            Assert.AreEqual(grid.RowCount, 10);
            Assert.AreEqual(grid.ColumnCount, 10);
            Assert.AreEqual(grid.CellSize.Width, 10.05f);
            Assert.AreEqual(grid.CellSize.Length, 1.05f);
            Assert.AreEqual(grid.CellCount, 100);
        }
    }
}
