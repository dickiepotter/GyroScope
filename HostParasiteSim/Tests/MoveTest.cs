using System;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class MoveTest
	{
		Rectangle bounds = new Rectangle(10,10);
		RandTest randTest = new RandTest();

		public class RandTest: System.Random
		{
			public int val =0;
			public override int Next(int a)
			{
				return val;
			}
			public override int Next(int a, int b)
			{
				return val;
			}
			public override double NextDouble()
			{
				return val;
			}
		}

		[Test]
		public void widthMoveTest()
		{
			randTest.val =0;
            // Rand 0,0 should relate to -X

			Position subject = new Position(bounds, 1, 10, 10, randTest); 
			subject.Move();
			Assert.IsTrue(subject.X == 9);
		}

		[Test]
		public void widthLessThanMoveTest()
		{
			randTest.val =0;
			// Rand 0,0 should relate to -X

			Position subject = new Position(bounds, 100, 10, 10, randTest); 
			subject.Move();
			// Should not manage to move at all
			Assert.IsTrue(subject.X == 10);
		}

		[Test]
		public void fractionMoveTest()
		{
			randTest.val =0;
			// Rand 0,0 should relate to -X

			Position subject = new Position(bounds, 0.01f, 10, 10, randTest); 
			subject.Move();
			Assert.IsTrue(subject.X == 9.99f);
		}

		[Test]
		public void widthBoundMoveTest()
		{
			randTest.val =0;
			// Rand 0,0 should relate to -X

			Position subject = new Position(bounds, 2, 1, 1, randTest); 
			subject.Move();
			// Should not manage to move at all
			Assert.IsTrue(subject.X == 1);
		}


	}
}
