using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestSingleLineCommenting : CelesteUnitTest
    {
        [TestMethod]
        public void TestSingleLineCommentingParsing()
        {
            CelesteScript script = RunScript("TestSingleLineCommenting.cel");

            Assert.AreEqual(2, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("var", 5);
            script.CheckLocalVariable("stringVar", "////test");
        }
    }
}