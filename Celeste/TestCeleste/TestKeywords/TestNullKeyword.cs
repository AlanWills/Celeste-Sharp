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
            CelesteScript script = new CelesteScript("TestScripts\\Keywords\\Null\\TestNullParsing.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);
            CelesteTestUtils.CheckLocalVariable(script, "variable", null);
        }
    }
}
