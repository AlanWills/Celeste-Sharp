using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestBoolType : CelesteUnitTest
    {
        [TestMethod]
        public void TestBoolTypeParsing()
        {
            CelesteScript script = RunScript("Types\\Bool\\TestBoolParsing.cel");

            script.CheckLocalVariable("trueObject", true);
            script.CheckLocalVariable("falseObject", false);
            script.CheckLocalVariable("true", true);
            script.CheckLocalVariable("false", false);
        }
    }
}
