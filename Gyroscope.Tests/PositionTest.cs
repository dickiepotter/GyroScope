namespace Gyroscope.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositionTest
    {
        Rectangle fiftyXfiftyBorder = new Rectangle(50, 50);
        Grid fiveXfiveGrid;

        [TestInitialize]
        public void TestSetup()
        {
            this.fiveXfiveGrid = new Grid(this.fiftyXfiftyBorder, 5, 5);
        }

        [TestMethod]
        public void PositionToCellFirstCellTest()
        {
            Position position = new Position(this.fiftyXfiftyBorder, 1, 0, 0);

            //Indexing from 0
            Assert.AreEqual(this.fiveXfiveGrid.Column(position), 0);
            Assert.AreEqual(this.fiveXfiveGrid.Row(position), 0);
            Assert.AreEqual(this.fiveXfiveGrid.Cell(position), 0);
        }

        [TestMethod]
        public void PositionToCellLastCellTest()
        {
            Position position = new Position(this.fiftyXfiftyBorder, 1, 50, 50);

            //Indexing from 0
            Assert.AreEqual(this.fiveXfiveGrid.Column(position), 4);
            Assert.AreEqual(this.fiveXfiveGrid.Row(position), 4);
            Assert.AreEqual(this.fiveXfiveGrid.Cell(position), 24);
        }

        [TestMethod]
        public void PositionToCellRightOfFirstTest()
        {
            Position position = new Position(this.fiftyXfiftyBorder, 1, 10, 0);

            //Indexing from 0
            Assert.AreEqual(this.fiveXfiveGrid.Column(position), 1);
            Assert.AreEqual(this.fiveXfiveGrid.Row(position), 0);
            Assert.AreEqual(this.fiveXfiveGrid.Cell(position), 1);
        }

        [TestMethod]
        public void PositionToCellUpOfFirstTest()
        {
            Position position = new Position(this.fiftyXfiftyBorder, 1, 0, 10);

            //Indexing from 0
            Assert.AreEqual(this.fiveXfiveGrid.Column(position), 0);
            Assert.AreEqual(this.fiveXfiveGrid.Row(position), 1);
            Assert.AreEqual(this.fiveXfiveGrid.Cell(position), 5);
        }
    }
}