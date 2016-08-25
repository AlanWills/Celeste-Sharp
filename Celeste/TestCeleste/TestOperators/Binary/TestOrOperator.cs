using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestOrOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestOrOperatorOrNumbers()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Or\\TestOrOperatorOrNumbers.cel");

            script.CheckLocalVariable("intOr", false);
            script.CheckLocalVariable("intOr2", false);
            script.CheckLocalVariable("floatOr", false);
            script.CheckLocalVariable("floatOr2", false);
        }

        [TestMethod]
        public void TestOrOperatorOrStrings()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Or\\TestOrOperatorOrStrings.cel");

            script.CheckLocalVariable("stringOr", false);
            script.CheckLocalVariable("stringOr2", false);
            script.CheckLocalVariable("emptyOr", false);
            script.CheckLocalVariable("emptyOr2", false);
        }

        [TestMethod]
        public void TestOrOperatorOrBools()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Or\\TestOrOperatorOrBools.cel");

            script.CheckLocalVariable("trueOr", true);
            script.CheckLocalVariable("falseOr", false);
            script.CheckLocalVariable("trueFalseOr", true);
        }

        [TestMethod]
        public void TestOrOperatorOrReferences()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Or\\TestOrOperatorOrReferences.cel");

            // Check reflexivity of references - references that are not null will always be true
            script.CheckLocalVariable("numberReflexivity", true);
            script.CheckLocalVariable("stringReflexivity", true);
            script.CheckLocalVariable("boolReflexivity", true);
            script.CheckLocalVariable("listReflexivity", true);
            script.CheckLocalVariable("tableReflexivity", true);

            // Check either the value or the reference to the variable
            script.CheckLocalVariable("numberOrNumberRef", true);
            script.CheckLocalVariable("stringOrStringRef", true);
            script.CheckLocalVariable("boolOrBoolRef", true);
            script.CheckLocalVariable("listOrListRef", true);
            script.CheckLocalVariable("tableOrTableRef", true);

            // Check the different types with the or operator - these should be true since our variables are not null
            script.CheckLocalVariable("numberOrString", true);
            script.CheckLocalVariable("numberOrBool", true);
            script.CheckLocalVariable("numberOrList", true);
            script.CheckLocalVariable("numberOrTable", true);

            script.CheckLocalVariable("stringOrBool", true);
            script.CheckLocalVariable("stringOrList", true);
            script.CheckLocalVariable("stringOrTable", true);

            script.CheckLocalVariable("boolOrList", true);
            script.CheckLocalVariable("boolOrTable", true);

            script.CheckLocalVariable("listOrTable", true);

            script.CheckLocalVariable("nullReflexivity", false);
            script.CheckLocalVariable("nullAndValue", false);
            script.CheckLocalVariable("nullAndNonNullVariable", true);
        }
    }
}