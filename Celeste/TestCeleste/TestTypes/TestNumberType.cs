using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestNumberType : CelesteUnitTest
    {
        [TestMethod]
        public void TestNumberTypeParsing()
        {
            CelesteScript script = RunScript("TestScripts\\Types\\Number\\TestNumberParsing.cel");

            script.CheckLocalVariable("int", 10.0f);
            script.CheckLocalVariable("anotherInt", 5.0f);
            script.CheckLocalVariable("zero", 0.0f);
            script.CheckLocalVariable("negative", -5.0f);
            script.CheckLocalVariable("float", 10.0f);
            script.CheckLocalVariable("negativeFloat", -5.0f);
        }
    }
}
