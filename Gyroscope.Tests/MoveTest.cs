namespace Gyroscope.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MoveTest
    {
        Rectangle bounds = new Rectangle(10, 10);
        RandTest randTest = new RandTest();

        public class RandTest : System.Random
        {
            public int val = 0;
            public override int Next(int a)
            {
                return this.val;
            }
            public override int Next(int a, int b)
            {
                return this.val;
            }
            public override double NextDouble()
            {
                return this.val;
            }
        }

        [TestMethod]
        public void widthMoveTest()
        {
            this.randTest.val = 0;
            // Rand 0,0 should relate to -X

            Position subject = new Position(this.bounds, 1, 10, 10, this.randTest);
            subject.Move();
            Assert.IsTrue(subject.X == 9);
        }

        [TestMethod]
        public void widthLessThanMoveTest()
        {
            this.randTest.val = 0;
            // Rand 0,0 should relate to -X

            Position subject = new Position(this.bounds, 100, 10, 10, this.randTest);
            subject.Move();
            // Should not manage to move at all
            Assert.IsTrue(subject.X == 10);
        }

        [TestMethod]
        public void fractionMoveTest()
        {
            this.randTest.val = 0;
            // Rand 0,0 should relate to -X

            Position subject = new Position(this.bounds, 0.01f, 10, 10, this.randTest);
            subject.Move();
            Assert.IsTrue(subject.X == 9.99f);
        }

        [TestMethod]
        public void widthBoundMoveTest()
        {
            this.randTest.val = 0;
            // Rand 0,0 should relate to -X

            Position subject = new Position(this.bounds, 2, 1, 1, this.randTest);
            subject.Move();
            // Should not manage to move at all
            Assert.IsTrue(subject.X == 1);
        }


    }
}