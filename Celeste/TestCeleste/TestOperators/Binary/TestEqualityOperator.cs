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
            CelesteScript script = RunScript("Operators\\Binary\\Equality\\TestEqualityOperatorEquateNumbers.cel");

            script.CheckLocalVariable("floatEquality", true);
            script.CheckLocalVariable("intEquality", true);
            script.CheckLocalVariable("intInequality", false);
            script.CheckLocalVariable("floatInequality", false);
        }

        [TestMethod]
        public void TestEqualityOperatorEquateStrings()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Equality\\TestEqualityOperatorEquateStrings.cel");

            script.CheckLocalVariable("stringEquality", true);
            script.CheckLocalVariable("stringInequality", false);
            script.CheckLocalVariable("emptyInequality", false);
            script.CheckLocalVariable("emptyEquality", true);
        }

        [TestMethod]
        public void TestEqualityOperatorEquateBools()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Equality\\TestEqualityOperatorEquateBools.cel");

            script.CheckLocalVariable("trueEquality", true);
            script.CheckLocalVariable("falseEquality", true);
            script.CheckLocalVariable("boolInequality", false);
            script.CheckLocalVariable("notTrueFalse", true);
            script.CheckLocalVariable("trueNotFalse", true);
            script.CheckLocalVariable("trueNotTrue", false);
        }

        [TestMethod]
        public void TestEqualityOperatorEquateReferences()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Equality\\TestEqualityOperatorEquateReferences.cel");

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
            script.CheckLocalVariable("numberEqualsString", false);
            script.CheckLocalVariable("numberEqualsBool", false);
            script.CheckLocalVariable("numberEqualsList", false);
            script.CheckLocalVariable("numberEqualsTable", false);

            script.CheckLocalVariable("stringEqualsBool", false);
            script.CheckLocalVariable("stringEqualsList", false);
            script.CheckLocalVariable("stringEqualsTable", false);

            script.CheckLocalVariable("boolEqualsList", false);
            script.CheckLocalVariable("boolEqualsTable", false);

            script.CheckLocalVariable("listEqualsTable", false);
            script.CheckLocalVariable("notListEqualsTable", false);
        }
    }
}