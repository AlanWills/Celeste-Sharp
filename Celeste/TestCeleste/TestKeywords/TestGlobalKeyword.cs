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
            CelesteScript script = new CelesteScript("TestScripts\\Keywords\\Global\\TestGlobalParsing.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);
            CelesteTestUtils.CheckGlobalVariable("variable", null);
        }
    }
}
