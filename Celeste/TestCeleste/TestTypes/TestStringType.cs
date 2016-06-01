using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestStringType : CelesteUnitTest
    {
        [TestMethod]
        public void TestStringTypeParsing()
        {
            CelesteScript script = RunScript("TestScripts\\Types\\String\\TestStringParsing.cel");

            script.CheckLocalVariable("test", "Test");
            script.CheckLocalVariable("testWhitespace", "test");
            script.CheckLocalVariable("twoWords", "test space");
            script.CheckLocalVariable("threeWords", "test two spaces");
            script.CheckLocalVariable("manyWords", "test really long sentence with lots of spaces");
        }
    }
}
