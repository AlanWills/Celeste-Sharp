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
            CelesteScript script = RunScript("Keywords\\Global\\TestGlobalParsing.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableCount >= 2);
            CheckGlobalVariable("variable", null);
            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("func"));
        }
    }
}