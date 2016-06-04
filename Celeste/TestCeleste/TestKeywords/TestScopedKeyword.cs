using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestScopedKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestScopedKeywordParsing()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Scoped\\TestScopedParsing.cel");

            Assert.AreEqual(2, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("variable", null);
            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
        }
    }
}
