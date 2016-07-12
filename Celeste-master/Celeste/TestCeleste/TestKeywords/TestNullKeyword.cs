using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestNullKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestNullKeywordParsing()
        {
            CelesteScript script = RunScript("Keywords\\Null\\TestNullParsing.cel");

            script.CheckLocalVariable("variable", null);
        }
    }
}
