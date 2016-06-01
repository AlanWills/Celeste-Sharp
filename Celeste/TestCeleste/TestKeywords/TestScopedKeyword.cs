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

            script.CheckLocalVariable("variable", null);
        }
    }
}
