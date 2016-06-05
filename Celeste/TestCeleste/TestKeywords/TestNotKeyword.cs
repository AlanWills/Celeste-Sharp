using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestNotKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestNotKeywordOrNumbers()
        {
            CelesteScript script = RunScript("Keywords\\Not\\TestNotKeywordNotNumbers.cel");

            script.CheckLocalVariable("notInt", false);
            script.CheckLocalVariable("notFloat", false);
        }

        [TestMethod]
        public void TestNotKeywordOrStrings()
        {
            CelesteScript script = RunScript("Keywords\\Not\\TestNotKeywordNotStrings.cel");

            script.CheckLocalVariable("notString", false);
            script.CheckLocalVariable("notEmpty", false);
        }

        [TestMethod]
        public void TestNotKeywordOrBools()
        {
            CelesteScript script = RunScript("Keywords\\Not\\TestNotKeywordNotBools.cel");

            script.CheckLocalVariable("notTrue", false);
            script.CheckLocalVariable("notFalse", true);
        }

        [TestMethod]
        public void TestNotKeywordOrReferences()
        {
            CelesteScript script = RunScript("Keywords\\Not\\TestNotKeywordNotReferences.cel");

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