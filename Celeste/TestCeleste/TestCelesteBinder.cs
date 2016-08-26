using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteBinder : CelesteUnitTest
    {
        #region Setup and Tear Down

        protected override void TestInitialize()
        {
            // Don't forget to actually add the bindings!
            // For some reason class clean up wasn't working so I had to do this for every test.
            // Eurgh
            CelesteBinder.Init();
        }

        protected override void TestCleanUp()
        {
            // Make sure we clean the bindings up for other tests
            // For some reason class clean up wasn't working so I had to do this for every test.
            // Eurgh
            CelesteBinder.ClearBindings();
        }

        #endregion

        #region Bind To String Tests

        [TestMethod]
        public void Test_CelesteBinder_BindFloatToString()
        {
            Assert.AreEqual("10", CelesteBinder.Bind(10.0f, typeof(string)));
            Assert.AreEqual("-10", CelesteBinder.Bind(-10.0f, typeof(string)));
            Assert.AreEqual("0", CelesteBinder.Bind(0.0f, typeof(string)));
        }

        [TestMethod]
        public void Test_CelesteBinder_BindBoolToString()
        {
            Assert.AreEqual("True", CelesteBinder.Bind(true, typeof(string)));
            Assert.AreEqual("False", CelesteBinder.Bind(false, typeof(string)));
        }

        [TestMethod]
        public void Test_CelesteBinder_BindReferenceToString()
        {
            Assert.AreEqual("Reference", CelesteBinder.Bind(new Reference("Reference"), typeof(string)));
            Assert.AreEqual("NestedReference", CelesteBinder.Bind(new Reference(new Reference(new Reference("NestedReference"))), typeof(string)));
        }

        #endregion
    }
}
