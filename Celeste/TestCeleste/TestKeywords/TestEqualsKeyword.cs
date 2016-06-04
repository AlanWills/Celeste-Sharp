using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;

namespace TestCeleste.TestKeywords
{
    [TestClass]
    public class TestEqualsKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestEqualsKeywordEquateNumbers()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Equals\\TestEqualsKeywordEquateNumbers.cel");

            script.CheckLocalVariable("floatEquality", true);
            script.CheckLocalVariable("intEquality", true);
            script.CheckLocalVariable("intInequality", false);
            script.CheckLocalVariable("floatInequality", false);
        }
        
        [TestMethod]
        public void TestEqualsKeywordEquateStrings()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Equals\\TestEqualsKeywordEquateStrings.cel");

            script.CheckLocalVariable("stringEquality", true);
            script.CheckLocalVariable("stringInequality", false);
            script.CheckLocalVariable("emptyInequality", false);
            script.CheckLocalVariable("emptyEquality", true);
        }

        [TestMethod]
        public void TestEqualityKeywordEquateBools()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Equals\\TestEqualsKeywordEquateBools.cel");

            script.CheckLocalVariable("trueEquality", true);
            script.CheckLocalVariable("falseEquality", true);
            script.CheckLocalVariable("boolInequality", false);
        }

        [TestMethod]
        public void TestEqualityKeywordEquateReferences()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Equals\\TestEqualsKeywordEquateReferences.cel");

            // Check reflexivity of references - references should always be equal to themselves
            script.CheckLocalVariable("numberReflexivity", true);
            script.CheckLocalVariable("stringReflexivity", true);
            script.CheckLocalVariable("boolReflexivity", true);
            script.CheckLocalVariable("listReflexivity", true);
            script.CheckLocalVariable("tableReflexivity", true);

            // Check references to a variable are equal to the variable
            script.CheckLocalVariable("numberEqualsNumberRef", true);
            script.CheckLocalVariable("stringEqualsStringRef", true);
            script.CheckLocalVariable("boolEqualsBoolRef", true);
            script.CheckLocalVariable("listEqualsListRef", true);
            script.CheckLocalVariable("tableEqualsTableRef", true);

            // Check the different types are not equal to each other
            script.CheckLocalVariable("numberNotEqualsString", false);
            script.CheckLocalVariable("numberNotEqualsBool", false);
            script.CheckLocalVariable("numberNotEqualsList", false);
            script.CheckLocalVariable("numberNotEqualsTable", false);

            script.CheckLocalVariable("stringNotEqualsBool", false);
            script.CheckLocalVariable("stringNotEqualsList", false);
            script.CheckLocalVariable("stringNotEqualsTable", false);

            script.CheckLocalVariable("boolNotEqualsList", false);
            script.CheckLocalVariable("boolNotEqualsTable", false);

            script.CheckLocalVariable("listNotEqualsTable", false);
        }
    }
}
