using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestEqualityOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestEqualityOperatorEquateNumbers()
        {
            CelesteScript script = RunScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorEquateNumbers.cel");

            script.CheckLocalVariable("floatEquality", true);
            script.CheckLocalVariable("intEquality", true);
            script.CheckLocalVariable("intInequality", false);
            script.CheckLocalVariable("floatInequality", false);
        }

        [TestMethod]
        public void TestEqualityOperatorEquateStrings()
        {
            CelesteScript script = RunScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorEquateStrings.cel");

            script.CheckLocalVariable("stringEquality", true);
            script.CheckLocalVariable("stringInequality", false);
            script.CheckLocalVariable("emptyInequality", false);
            script.CheckLocalVariable("emptyEquality", true);
        }
    }
}