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
        public void Test_FunctionKeyword_ArgumentParsingNoSpaces()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordArgumentParsingNoSpaces.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));

            Function testFunc = script.ScriptScope.GetLocalVariable("testFunction", ScopeSearchOption.kThisScope) as Function;
            Assert.AreEqual(3, testFunc.ParameterNames.Count);
            Assert.IsTrue(testFunc.ParameterNames.Contains("param1"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param2"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param3"));
        }

        [TestMethod]
        public void Test_FunctionKeyword_ArgumentParsingSpaces()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordArgumentParsingSpaces.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));

            Function testFunc = script.ScriptScope.GetLocalVariable("testFunction", ScopeSearchOption.kThisScope) as Function;
            Assert.AreEqual(3, testFunc.ParameterNames.Count);
            Assert.IsTrue(testFunc.ParameterNames.Contains("param1"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param2"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param3"));
        }

        [TestMethod]
        public void Test_FunctionKeyword_ArgumentParsingMixedSpaces()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordArgumentParsingMixedSpaces.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));

            Function testFunc = script.ScriptScope.GetLocalVariable("testFunction", ScopeSearchOption.kThisScope) as Function;
            Assert.AreEqual(3, testFunc.ParameterNames.Count);
            Assert.IsTrue(testFunc.ParameterNames.Contains("param1"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param2"));
            Assert.IsTrue(testFunc.ParameterNames.Contains("param3"));
        }

        [TestMethod]
        public void Test_FunctionKeyword_ArgumentParsingEdgeCase_HardCodedStringWithSpaces()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordArgumentParsingEdgeCaseHardCodedStringWithSpaces.cel");

            script.CheckLocalVariable("variable", "Argument with spaces");
        }

        [TestMethod]
        public void Test_FunctionKeyword_SimpleExecution()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordSimpleExecution.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", "Same variable name outside of function scope");
        }

        [TestMethod]
        public void Test_FunctionKeyword_VariableScoping()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordVariableScoping.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunction"));
            script.CheckLocalVariable("functionResult", 5.0f);
        }

        [TestMethod]
        public void Test_FunctionKeyword_OneValueArgument()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordOneValueArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", 2.0f);
        }

        [TestMethod]
        public void Test_FunctionKeyword_OneReferenceArgument()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordOneReferenceArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", true);
        }

        [TestMethod]
        public void Test_FunctionKeyword_MultipleCalls()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordMultipleCalls.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("assignmentFunc"));
            script.CheckLocalVariable("firstCall", 2.0f);
            script.CheckLocalVariable("secondCall", true);
        }

        [TestMethod]
        public void Test_FunctionKeyword_TooManyInputs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordTooManyInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("variable", true);
        }

        [TestMethod]
        public void Test_FunctionKeyword_NotEnoughInputs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordNotEnoughInputs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", null);
        }

        [TestMethod]
        public void Test_FunctionKeyword_FunctionReassignmentNoArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentNoArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }

        [TestMethod]
        public void Test_FunctionKeyword_FunctionReassignmentWithArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentWithArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }

        [TestMethod]
        public void Test_FunctionKeyword_FunctionReassignmentDifferentNumberArgs()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignmentDifferentNumberArgs.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", 10.0f);
            script.CheckLocalVariable("secondVariable", "TestReassignment");
        }
    }
}