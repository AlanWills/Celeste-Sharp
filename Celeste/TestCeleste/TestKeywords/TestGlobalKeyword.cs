using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestGlobalKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestGlobalKeywordParsing()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Global\\TestGlobalParsing.cel");

            Assert.AreEqual(2, CelesteStack.GlobalScope.VariableCount);
            CheckGlobalVariable("variable", null);
            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("func()"));
        }
    }
}