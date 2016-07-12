using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestReturnKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestReturnKeywordReturnNothing()
        {
            CelesteScript script = RunScript("Keywords\\Return\\TestReturnKeywordReturnNothing.cel");

            Assert.AreEqual(2, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("variable", true);
            Assert.IsTrue(script.ScriptScope.VariableExists("funcNoReturnParams"));
        }

        [TestMethod]
        public void TestReturnKeywordReturnHardCodedValue()
        {
            CelesteScript script = RunScript("Keywords\\Return\\TestReturnKeywordReturnHardCodedValue.cel");

            Assert.AreEqual(2, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("variable", true);
            Assert.IsTrue(script.ScriptScope.VariableExists("funcReturnsTrue"));
        }

        [TestMethod]
        public void TestReturnKeywordReturnInput()
        {
            CelesteScript script = RunScript("Keywords\\Return\\TestReturnKeywordReturnInput.cel");

            Assert.AreEqual(4, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("firstVariable", true);
            script.CheckLocalVariable("secondVariable", "Test");
            script.CheckLocalVariable("thirdVariable", true);
            Assert.IsTrue(script.ScriptScope.VariableExists("funcReturnsInput"));
        }

        [TestMethod]
        public void TestReturnKeywordReturnMultipleParamsSimple()
        {
            // This just tests that nothing goes wrong when returning multiple parameters
            CelesteScript script = RunScript("Keywords\\Return\\TestReturnKeywordReturnMultipleParamsSimple.cel");

            Assert.AreEqual(1, script.ScriptScope.VariableCount);
            Assert.IsTrue(script.ScriptScope.VariableExists("funcReturnsMultipleParams"));
        }
    }
}
