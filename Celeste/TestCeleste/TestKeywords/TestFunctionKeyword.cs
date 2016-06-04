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
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordParsing.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
        }

        [TestMethod]
        public void TestFunctionKeywordSimpleExecution()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordSimpleExecution.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", "Same variable name outside of function scope");
        }

        [TestMethod]
        public void TestFunctionKeywordVariableScoping()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordVariableScoping.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", 5.0f);
        }

        [TestMethod]
        public void TestFunctionKeywordOneArgument()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordOneArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", 2.0f);
        }

        [TestMethod]
        public void TestFunctionKeywordMultipleCalls()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordMultipleCalls.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("assignmentFunc"));
            script.CheckLocalVariable("firstCall", 2.0f);
            script.CheckLocalVariable("secondCall", true);
        }
    }
}