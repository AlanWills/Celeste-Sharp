using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestFunctionKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestFunctionKeywordParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Keywords\\Function\\TestFunctionParsing.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);
            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction()"));
        }

        [TestMethod]
        public void TestFunctionKeywordSimpleExecution()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Keywords\\Function\\TestFunctionSimpleExecution.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);
            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction()"));
            Assert.IsFalse(script.ScriptScope.VariableExists("functionResult"));
        }
    }
}