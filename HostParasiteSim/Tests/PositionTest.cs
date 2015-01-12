using System;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class PositionTest
	{
		Rectangle fiftyXfiftyBorder = new Rectangle(50, 50);
		Grid fiveXfiveGrid; 

		[SetUp]
		public void TestSetup()
		{
			fiveXfiveGrid = new Grid( fiftyXfiftyBorder ,5, 5 );
		}

		[Test]
		public void PositionToCellFirstCellTest()
		{
			Position position = new Position( fiftyXfiftyBorder, 1, 0, 0 );
			
			//Indexing from 0
			Assert.AreEqual(fiveXfiveGrid.Column(position), 0);
			Assert.AreEqual(fiveXfiveGrid.Row(position), 0);
			Assert.AreEqual(fiveXfiveGrid.Cell(position), 0);
		}

		[Test]
		public void PositionToCellLastCellTest()
		{
			Position position = new Position( fiftyXfiftyBorder, 1, 50, 50 );
			
			//Indexing from 0
			Assert.AreEqual(fiveXfiveGrid.Column(position), 4);
			Assert.AreEqual(fiveXfiveGrid.Row(position), 4);
			Assert.AreEqual(fiveXfiveGrid.Cell(position), 24);
		}

		[Test]
		public void PositionToCellRightOfFirstTest()
		{
			Position position = new Position( fiftyXfiftyBorder, 1, 10, 0 );
			
			//Indexing from 0
			Assert.AreEqual(fiveXfiveGrid.Column(position), 1);
			Assert.AreEqual(fiveXfiveGrid.Row(position), 0);
			Assert.AreEqual(fiveXfiveGrid.Cell(position), 1);
		}

		[Test]
		public void PositionToCellUpOfFirstTest()
		{
			Position position = new Position( fiftyXfiftyBorder, 1, 0, 10 );
			
			//Indexing from 0
			Assert.AreEqual(fiveXfiveGrid.Column(position), 0);
			Assert.AreEqual(fiveXfiveGrid.Row(position), 1);
			Assert.AreEqual(fiveXfiveGrid.Cell(position), 5);
		}
	}
}
