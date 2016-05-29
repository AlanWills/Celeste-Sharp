using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    /// <summary>
    /// Summary description for CelesteUnitTest
    /// </summary>
    [TestClass]
    public class CelesteUnitTest
    {
        public CelesteUnitTest()
        {
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }

        // Check that the state of our stack and scopes are clean
        [TestInitialize()]
        public void MyTestInitialize()
        {
            CelesteTestUtils.CheckStackSize(0);
            Assert.IsTrue(CelesteStack.Scopes.Count == 1);
            Assert.IsTrue(CelesteStack.CurrentScope == CelesteStack.GlobalScope);

            CelesteStack.Clear();
            CelesteStack.Scopes.Clear();
            CelesteStack.Scopes.Add(CelesteStack.GlobalScope);
            CelesteStack.CurrentScope = CelesteStack.GlobalScope;
        }

        // Check that the state of our stack and scopes are clean
        // Our stack may have leftovers, in which case we just clear them
        [TestCleanup()]
        public void MyTestCleanup()
        {
            CelesteTestUtils.CheckStackSize(0);
            Assert.IsTrue(CelesteStack.Scopes.Count == 1);
            Assert.IsTrue(CelesteStack.CurrentScope == CelesteStack.GlobalScope);

            CelesteStack.Clear();
            CelesteStack.Scopes.Clear();
            CelesteStack.Scopes.Add(CelesteStack.GlobalScope);
            CelesteStack.CurrentScope = CelesteStack.GlobalScope;
        }

        #endregion
    }
}
