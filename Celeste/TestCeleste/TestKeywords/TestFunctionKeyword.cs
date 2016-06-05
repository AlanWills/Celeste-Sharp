﻿using Celeste;
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
        public void TestFunctionKeywordOneArgument()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordOneArgument.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("funcOneArg"));
            script.CheckLocalVariable("result", 2.0f);
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
        public void TestFunctionKeywordFunctionReassignment()
        {
            CelesteScript script = RunScript("Keywords\\Function\\TestFunctionKeywordFunctionReassignment.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("firstFunc"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondFunc"));
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", false);
        }
    }
}