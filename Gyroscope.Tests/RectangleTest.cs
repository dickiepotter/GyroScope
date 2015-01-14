namespace Gyroscope.Tests
{
    using System;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void InvalidSizeTest()
        {
            Rectangle subject;

            try
            {
                subject = new Rectangle(0, 0);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(-1, -1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(-0.01f, 0.01f);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(float.MinValue, float.MinValue);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(float.MinValue - 1, float.MinValue - 1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(0, 1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(1, 0);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void ValidSizeTestWithIntParams()
        {
            try
            {
                var subject = new Rectangle(1, 1);

                subject.Length.Should().Be(1, "because we are using the width and length constructor of rectangle with the parameters 1,1");
                subject.Width.Should().Be(1, "because we are using the width and length constructor of rectangle with the parameters 1,1");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSizeTestWithFloatParams()
        {
            try
            {
                var subject = new Rectangle(0.01f, 0.01f);

                subject.Length.Should().Be(0.01f, "because we are using the width and length constructor of rectangle with the parameters 0.01f,0.01f");
                subject.Width.Should().Be(0.01f, "because we are using the width and length constructor of rectangle with the parameters 0.01f,0.01f");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSizeTestWithFloatMaxValueParams()
        {
            try
            {
                var subject = new Rectangle(float.MaxValue, float.MaxValue);
                subject.Length.Should().Be(float.MaxValue, "because we are using the width and length constructor of rectangle with the parameters float.MaxValue,float.MaxValue");
                subject.Width.Should().Be(float.MaxValue, "because we are using the  width and length constructor of rectangle with the parameters float.MaxValue,float.MaxValue");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSizeTestWithDifferentWidthAndLengthParams()
        {
            try
            {
                var subject = new Rectangle(1f, float.MaxValue + 1f);
                subject.Width.Should().Be(1f, "because we are using the width and length constructor of rectangle with the width parameter as 1");
                subject.Length.Should().Be(float.MaxValue, "because we are using the width and length constructor of rectangle with the length parameter over the max size of a float");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSizeTestWithFloatOverMaxValueParams()
        {
            try
            {
                var subject = new Rectangle(float.MaxValue + 1f, float.MaxValue + 1f);
                subject.Length.Should().Be(float.MaxValue, "because we are using the width and length constructor of rectangle with the parameters over the max size of a float");
                subject.Width.Should().Be(float.MaxValue, "because we are using the width and length constructor of rectangle with the parameters over the max size of a float");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSquareSizeTestWithIntParam()
        {
            try
            {
                var subject = new Rectangle(1);

                subject.Length.Should().Be(1, "because we are using the equal width and length constructor of rectangle with the parameter 1");
                subject.Width.Should().Be(1, "because we are using the equal width and length constructor of rectangle with the parameter 1");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSquareSizeTestWithFloatParam()
        {
            try
            {
                var subject = new Rectangle(0.01f);

                subject.Length.Should().Be(0.01f, "because we are using the equal width and length constructor of rectangle with the parameter 0.01f");
                subject.Width.Should().Be(0.01f, "because we are using the equal width and length constructor of rectangle with the parameter 0.01f");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSquareSizeTestWithFloatMaxValueParam()
        {
            try
            {
                var subject = new Rectangle(float.MaxValue);
                subject.Length.Should().Be(float.MaxValue, "because we are using the equal width and length constructor of rectangle with the parameter float.MaxValue");
                subject.Width.Should().Be(float.MaxValue, "because we are using the equal width and length constructor of rectangle with the parameter float.MaxValue");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void ValidSquareSizeTestWithFloatOverMaxValueParam()
        {
            try
            {
                var subject = new Rectangle(float.MaxValue + 1f);
                subject.Length.Should().Be(float.MaxValue, "because we are using the equal width and length constructor of rectangle with a parameter over the max size of a float");
                subject.Width.Should().Be(float.MaxValue, "because we are using the equal width and length constructor of rectangle with a parameter over the max size of a float");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void InvalidSquareSizeTest()
        {
            Rectangle subject;

            try
            {
                subject = new Rectangle(0);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(-1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(-0.01f);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(float.MinValue);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }

            try
            {
                subject = new Rectangle(float.MinValue - 1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void ValidSetSizeTest()
        {
            /*	Rectangle subject = new Rectangle();

                try
                {
                    subject.Width =  1 ; 
                    Assert.AreEqual(subject.Width, 1);

                    subject.Width = 0.01f ;
                    Assert.AreEqual(subject.Width, 0.01f);

				
                    subject.Width = float.MaxValue;
                    Assert.AreEqual(subject.Width, float.MaxValue);

                    subject.Width = float.MaxValue +1 ;
                    Assert.AreEqual(subject.Width, float.MaxValue );

				
                    subject.Length =  1 ; 
                    Assert.AreEqual(1, subject.Length);

                    subject.Length = 0.01f ;
                    Assert.AreEqual(0.01f, subject.Length);

                    subject.Length = float.MaxValue;
                    Assert.AreEqual(float.MaxValue, subject.Length);

                    subject.Length = float.MaxValue +1 ;
                    Assert.AreEqual(float.MaxValue, subject.Length);
                }
                catch( Exception )
                {
                    Assert.Fail();
                }
                */
        }

        [TestMethod]
        public void InvalidSetSizeTest()
        {
            /*
            Rectangle subject = new Rectangle();

            try
            { 
                subject.Width = 0 ; 
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            { 
                subject.Width = -1 ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }
			
            try
            { 
                subject.Width = -0.01f ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            {
                subject.Width = float.MinValue ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            {
                subject.Width = float.MinValue -1 ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }



            try
            { 
                subject.Length = 0 ; 
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            { 
                subject.Length = -1 ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }
			
            try
            { 
                subject.Length = -0.01f ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            {
                subject.Length = float.MinValue ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }

            try
            {
                subject.Length = float.MinValue -1 ;
            }
            catch( Exception e )
            {
                Assert.IsTrue( e is ArgumentOutOfRangeException );
            }
            */
        }
    }
}