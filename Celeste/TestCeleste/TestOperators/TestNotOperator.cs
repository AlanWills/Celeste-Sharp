using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestNotOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestNotOperatorOrNumbers()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Not\\TestNotKeywordNotNumbers.cel");

            script.CheckLocalVariable("notInt", false);
            script.CheckLocalVariable("notFloat", false);
        }

        [TestMethod]
        public void TestNotOperatorOrStrings()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Not\\TestNotKeywordNotStrings.cel");

            script.CheckLocalVariable("notString", false);
            script.CheckLocalVariable("notEmpty", false);
        }

        [TestMethod]
        public void TestNotOperatorOrBools()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Not\\TestNotKeywordNotBools.cel");

            script.CheckLocalVariable("notTrue", false);
            script.CheckLocalVariable("notFalse", true);
        }

        [TestMethod]
        public void TestNotOperatorOrReferences()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Not\\TestNotKeywordNotReferences.cel");

            script.CheckLocalVariable("notList", false);
            script.CheckLocalVariable("notTable", false);
            script.CheckLocalVariable("notNull", true);

            script.CheckLocalVariable("notNumberRef", false);
            script.CheckLocalVariable("notStringRef", false);
            script.CheckLocalVariable("notBoolRef", false);
            script.CheckLocalVariable("notListRef", false);
            script.CheckLocalVariable("notTableRef", false);
        }
    }
}