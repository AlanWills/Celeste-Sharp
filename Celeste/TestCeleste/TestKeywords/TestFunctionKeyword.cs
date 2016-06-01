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
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionParsing.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction()"));
        }

        [TestMethod]
        public void TestFunctionKeywordSimpleExecution()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionSimpleExecution.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction()"));
            script.CheckLocalVariable("functionResult", "Same variable name outside of function scope");
        }

        [TestMethod]
        public void TestFunctionKeywordVariableScoping()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionVariableScoping.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction()"));
            script.CheckLocalVariable("functionResult", 5.0f);
        }
    }
}