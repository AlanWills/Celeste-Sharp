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
            CelesteScript script = RunScript("Keywords\\Scoped\\TestScopedKeywordParsing.cel");

            Assert.AreEqual(2, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("variable", null);
            Assert.IsTrue(script.ScriptScope.VariableExists("func"));
        }

        [TestMethod]
        public void TestScopedKeywordMultipleVariableDeclaration()
        {
            CelesteScript script = RunScript("Keywords\\Scoped\\TestScopedKeywordMultipleVariableDeclaration.cel");

            Assert.AreEqual(3, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("var1", "first");
            script.CheckLocalVariable("var2", "second");
            script.CheckLocalVariable("var3", "third");
        }

        [TestMethod]
        public void TestScopedKeywordMultipleVariableInitialisation()
        {
            CelesteScript script = RunScript("Keywords\\Scoped\\TestScopedKeywordMultipleVariableInitialisation.cel");

            Assert.AreEqual(3, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("var1", "first");
            script.CheckLocalVariable("var2", "second");
            script.CheckLocalVariable("var3", "third");
        }
    }
}
