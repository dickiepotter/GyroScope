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
            Assert.AreEqual(grid.CellSize.Width, 0.0001f);
            Assert.AreEqual(grid.CellSize.Length, 0.0001f);
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

            (0.001 / 10).Should().Be(0.0001, "because we are testing the simple assumption that a calculation of 0.001 / 10 should not genrate rounding errors for c#");
            (100 / 10).Should().Be(10, "because we are testing the simple assumption that a calculation of 100 / 10 should not genrate rounding errors for c#");

            (0.001f / 10f).Should().Be(0.0001f, "because we are testing the simple assumption that a calculation of 0.001 / 10 should not genrate rounding errors for c# using floats");
            (100f / 10f).Should().Be(10f, "because we are testing the simple assumption that a calculation of 100 / 10 should not genrate rounding errors for c# using floats");

            grid.RowCount.Should().Be(10, "because we are making a 10x10 grid");
            grid.ColumnCount.Should().Be(10, "because we are making a 10x10 grid");
            grid.CellSize.Width.Should().Be(0.0001f, "because a width of 0.001f divided by 10 columns in the grid is 0.001f");
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
            Assert.AreEqual(grid.CellSize.Width, 0.0001f);
            Assert.AreEqual(grid.CellSize.Length, 0.001f);
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
