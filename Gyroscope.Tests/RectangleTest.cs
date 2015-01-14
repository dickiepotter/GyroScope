namespace Gyroscope.Tests
{
    using System;

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
        public void ValidSizeTest()
        {
            Rectangle subject;

            try
            {
                subject = new Rectangle(1, 1);
                Assert.AreEqual(subject.Width, 1);
                Assert.AreEqual(subject.Length, 1);


                subject = new Rectangle(0.01f, 0.01f);
                Assert.AreEqual(subject.Width, 0.01);
                Assert.AreEqual(subject.Length, 0.01);


                subject = new Rectangle(float.MaxValue, float.MaxValue);
                Assert.AreEqual(subject.Width, float.MaxValue);
                Assert.AreEqual(subject.Length, float.MaxValue);


                subject = new Rectangle(1, float.MaxValue);
                Assert.AreEqual(subject.Width, 1);
                Assert.AreEqual(subject.Length, float.MaxValue);

                subject = new Rectangle(float.MaxValue + 1, 1);
                Assert.AreEqual(subject.Width, float.MaxValue);
                Assert.AreEqual(subject.Length, 1);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidSquareSizeTest()
        {
            Rectangle subject;

            try
            {
                subject = new Rectangle(1);
                Assert.AreEqual(subject.Length, 1);
                Assert.AreEqual(subject.Width, 1);

                subject = new Rectangle(0.01f);
                Assert.AreEqual(subject.Length, 0.01);
                Assert.AreEqual(subject.Width, 0.01);

                subject = new Rectangle(float.MaxValue);
                Assert.AreEqual(subject.Length, float.MaxValue);
                Assert.AreEqual(subject.Width, float.MaxValue);

                subject = new Rectangle(float.MaxValue + 1);
                Assert.AreEqual(subject.Length, float.MaxValue);
                Assert.AreEqual(subject.Width, float.MaxValue);
            }
            catch (Exception)
            {
                Assert.Fail();
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