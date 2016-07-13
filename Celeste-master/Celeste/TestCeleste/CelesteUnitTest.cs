using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestFramework;

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
            CheckStackSize(0);
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
            CheckStackSize(0);
            Assert.IsTrue(CelesteStack.Scopes.Count == 1);
            Assert.IsTrue(CelesteStack.CurrentScope == CelesteStack.GlobalScope);

            CelesteStack.Clear();
            CelesteStack.Scopes.Clear();
            CelesteStack.Scopes.Add(CelesteStack.GlobalScope);
            CelesteStack.CurrentScope = CelesteStack.GlobalScope;
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Runs and returns the script at the inputted file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected CelesteScript RunScript(string filePath)
        {
            CelesteScript script = Celeste.Cel.CreateScript(filePath);
            script.Compile();
            script.Run();

            return script;
        }

        private void CheckStackSize(int expected)
        {
            Assert.AreEqual(expected, CelesteStack.StackSize);
        }

        protected void CheckGlobalVariable(string variableName, object expected)
        {
            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists(variableName));
            Assert.AreEqual(expected, CelesteStack.GlobalScope.GetLocalVariable(variableName).GetReferencedValue<object>());
        }

        #endregion
    }

    public static class TestExtensions
    {
        internal static void CheckLocalVariable(this CelesteScript script, string variableName, object expected)
        {
            Assert.IsTrue(script.ScriptScope.VariableExists(variableName));
            Assert.AreEqual(expected, script.ScriptScope.GetLocalVariable(variableName).GetReferencedValue<object>());
        }

        internal static void CheckLocalVariableList(this CelesteScript script, string variableName, List<object> expected)
        {
            Assert.IsTrue(script.ScriptScope.VariableExists(variableName));
            TestHelperFunctions.CheckOrderedListsEqual(expected, script.ScriptScope.GetLocalVariable(variableName).GetReferencedValue<List<object>>());
        }
    }
}
