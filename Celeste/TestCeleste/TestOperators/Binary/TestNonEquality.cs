using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestNonEqualityOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestNonEqualityOperatorEquateNumbers()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Inequality\\TestNonEqualityOperatorEquateNumbers.cel");

            script.CheckLocalVariable("floatEquality", false);
            script.CheckLocalVariable("intEquality", false);
            script.CheckLocalVariable("intInequality", true);
            script.CheckLocalVariable("floatInequality", true);
        }

        [TestMethod]
        public void TestNonEqualityOperatorEquateStrings()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Inequality\\TestNonEqualityOperatorEquateStrings.cel");

            script.CheckLocalVariable("stringEquality", false);
            script.CheckLocalVariable("stringInequality", true);
            script.CheckLocalVariable("emptyInequality", true);
            script.CheckLocalVariable("emptyEquality", false);
        }

        [TestMethod]
        public void TestNonEqualityOperatorEquateBools()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Inequality\\TestNonEqualityOperatorEquateBools.cel");

            script.CheckLocalVariable("trueEquality", false);
            script.CheckLocalVariable("falseEquality", false);
            script.CheckLocalVariable("boolInequality", true);
            script.CheckLocalVariable("notTrueFalse", false);
            script.CheckLocalVariable("trueNotFalse", false);
            script.CheckLocalVariable("trueNotTrue", true);
        }

        [TestMethod]
        public void TestNonEqualityOperatorEquateReferences()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Inequality\\TestNonEqualityOperatorEquateReferences.cel");

            // Check reflexivity of references - references should always be equal to themselves
            script.CheckLocalVariable("numberReflexivity", false);
            script.CheckLocalVariable("stringReflexivity", false);
            script.CheckLocalVariable("boolReflexivity", false);
            script.CheckLocalVariable("listReflexivity", false);
            script.CheckLocalVariable("tableReflexivity", false);

            // Check references to a variable are equal to the variable
            script.CheckLocalVariable("numberEqualsNumberRef", false);
            script.CheckLocalVariable("stringEqualsStringRef", false);
            script.CheckLocalVariable("boolEqualsBoolRef", false);
            script.CheckLocalVariable("listEqualsListRef", false);
            script.CheckLocalVariable("tableEqualsTableRef", false);

            // Check the different types are not equal to each other
            script.CheckLocalVariable("numberNotEqualsString", true);
            script.CheckLocalVariable("numberNotEqualsBool", true);
            script.CheckLocalVariable("numberNotEqualsList", true);
            script.CheckLocalVariable("numberNotEqualsTable", true);

            script.CheckLocalVariable("stringNotEqualsBool", true);
            script.CheckLocalVariable("stringNotEqualsList", true);
            script.CheckLocalVariable("stringNotEqualsTable", true);

            script.CheckLocalVariable("boolNotEqualsList", true);
            script.CheckLocalVariable("boolNotEqualsTable", true);

            script.CheckLocalVariable("listNotEqualsTable", true);
            script.CheckLocalVariable("notListNotEqualsTable", true);
        }
    }
}