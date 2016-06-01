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

            CheckGlobalVariable("variable", null);
        }
    }
}
