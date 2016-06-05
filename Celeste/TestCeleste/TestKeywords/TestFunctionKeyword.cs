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

        [TestMethod]
        public void TestFunctionKeywordTooManyInputs()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordTooManyInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("variable", true);
        }

        [TestMethod]
        public void TestFunctionKeywordNotEnoughInputs()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordNotEnoughInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", null);
        }

        [TestMethod]
        public void TestFunctionKeywordFunctionReassignment()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Function\\TestFunctionKeywordFunctionReassignment.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }

        // Tests still to do:
        // Assigning functions to one another - see if the behaviour changes midway through script (maybe better in the Assignment test suite)

        // Improve the Function class - we do not need to store CompiledStatements, we just need to store the object we will assign to the local variable
        // So have a list of objects instead stored in the big list - when we compile we either store the Value, or the Reference based on what type the thing we compiled was
        // It's exactly the same behaviour, just moving the behaviour into Compile and simplifying it somewhat
    }
}