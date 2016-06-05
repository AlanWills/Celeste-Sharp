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
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordParsing.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
        }

        [TestMethod]
        public void TestFunctionKeywordSimpleExecution()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordSimpleExecution.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", "Same variable name outside of function scope");
        }

        [TestMethod]
        public void TestFunctionKeywordVariableScoping()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordVariableScoping.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", 5.0f);
        }

        [TestMethod]
        public void TestFunctionKeywordOneValueArgument()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordOneValueArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", 2.0f);
        }

        [TestMethod]
        public void TestFunctionKeywordOneReferenceArgument()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordOneReferenceArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", true);
        }

        [TestMethod]
        public void TestFunctionKeywordMultipleCalls()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordMultipleCalls.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("assignmentFunc"));
            script.CheckLocalVariable("firstCall", 2.0f);
            script.CheckLocalVariable("secondCall", true);
        }

        [TestMethod]
        public void TestFunctionKeywordTooManyInputs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordTooManyInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("variable", true);
        }

        [TestMethod]
        public void TestFunctionKeywordNotEnoughInputs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordNotEnoughInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", null);
        }

        [TestMethod]
        public void TestFunctionKeywordFunctionReassignmentNoArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentNoArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }

        [TestMethod]
        public void TestFunctionKeywordFunctionReassignmentWithArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentWithArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }

        [TestMethod]
        public void TestFunctionKeywordFunctionReassignmentDifferentNumberArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentDifferentNumberArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", 10.0f);
            script.CheckLocalVariable("secondVariable", "TestReassignment");
        }
    }
}