namespace Gyroscope.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MoveTest
    {
        Rectangle bounds = new Rectangle(10, 10);
        RandTest randTest = new RandTest();

        /// <summary>
        /// Deterministic Random stub. Position.Move() calls Next(5) to pick a
        /// direction and NextDouble() to pick the fraction of the move distance,
        /// so these are controlled independently.
        /// In Move(), direction 3 maps to a -X (westward) move.
        /// </summary>
        public class RandTest : System.Random
        {
            public int Direction = 3;
            public double Fraction = 1.0;

            public override int Next(int a)
            {
                return this.Direction;
            }

            public override int Next(int a, int b)
            {
                return this.Direction;
            }

            public override double NextDouble()
            {
                return this.Fraction;
            }
        }

        [TestMethod]
        public void widthMoveTest()
        {
            // Direction 3 (-X), full move distance of 1 from x=10 lands on x=9.
            this.randTest.Direction = 3;
            this.randTest.Fraction = 1.0;

            Position subject = new Position(this.bounds, 1, 10, 10, this.randTest);
            subject.Move();
            Assert.AreEqual(9f, subject.X, 1e-4f);
        }

        [TestMethod]
        public void widthLessThanMoveTest()
        {
            // A -X move of 100 from x=10 would go negative, so it is rejected and x is unchanged.
            this.randTest.Direction = 3;
            this.randTest.Fraction = 1.0;

            Position subject = new Position(this.bounds, 100, 10, 10, this.randTest);
            subject.Move();
            // Should not manage to move at all
            Assert.AreEqual(10f, subject.X, 1e-4f);
        }

        [TestMethod]
        public void fractionMoveTest()
        {
            // Direction 3 (-X), fractional move distance of 0.01 from x=10 lands on x=9.99.
            this.randTest.Direction = 3;
            this.randTest.Fraction = 1.0;

            Position subject = new Position(this.bounds, 0.01f, 10, 10, this.randTest);
            subject.Move();
            Assert.AreEqual(9.99f, subject.X, 1e-4f);
        }

        [TestMethod]
        public void widthBoundMoveTest()
        {
            // A -X move of 2 from x=1 would go negative, so it is rejected and x is unchanged.
            this.randTest.Direction = 3;
            this.randTest.Fraction = 1.0;

            Position subject = new Position(this.bounds, 2, 1, 1, this.randTest);
            subject.Move();
            // Should not manage to move at all
            Assert.AreEqual(1f, subject.X, 1e-4f);
        }


    }
}
