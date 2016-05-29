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
            CelesteScript script = new CelesteScript("TestScripts\\Keywords\\Scoped\\TestScopedParsing.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);
            CelesteTestUtils.CheckLocalVariable(script, "variable", null);
        }
    }
}
