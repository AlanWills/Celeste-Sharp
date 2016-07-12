using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestAndOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestAndOperatorAndNumbers()
        {
            CelesteScript script = RunScript("Operators\\And\\TestAndOperatorAndNumbers.cel");

            script.CheckLocalVariable("intAnd", false);
            script.CheckLocalVariable("intAnd2", false);
            script.CheckLocalVariable("floatAnd", false);
            script.CheckLocalVariable("floatAnd2", false);
        }

        [TestMethod]
        public void TestAndOperatorAndStrings()
        {
            CelesteScript script = RunScript("Operators\\And\\TestAndOperatorAndStrings.cel");

            script.CheckLocalVariable("stringAnd", false);
            script.CheckLocalVariable("stringAnd2", false);
            script.CheckLocalVariable("emptyAnd", false);
            script.CheckLocalVariable("emptyAnd2", false);
        }

        [TestMethod]
        public void TestAndOperatorAndBools()
        {
            CelesteScript script = RunScript("Operators\\And\\TestAndOperatorAndBools.cel");

            script.CheckLocalVariable("trueAnd", true);
            script.CheckLocalVariable("falseAnd", false);
            script.CheckLocalVariable("trueFalseAnd", false);
        }

        [TestMethod]
        public void TestAndOperatorAndReferences()
        {
            CelesteScript script = RunScript("Operators\\And\\TestAndOperatorAndReferences.cel");

            // Check reflexivity of references - references that are not null will always be true
            script.CheckLocalVariable("numberReflexivity", true);
            script.CheckLocalVariable("stringReflexivity", true);
            script.CheckLocalVariable("boolReflexivity", true);
            script.CheckLocalVariable("listReflexivity", true);
            script.CheckLocalVariable("tableReflexivity", true);

            // Check either the value and the reference to the variable
            script.CheckLocalVariable("numberAndNumberRef", true);
            script.CheckLocalVariable("stringAndStringRef", true);
            script.CheckLocalVariable("boolAndBoolRef", true);
            script.CheckLocalVariable("listAndListRef", true);
            script.CheckLocalVariable("tableAndTableRef", true);

            // Check the different types with the and operator - these should be true since our variables are not null
            script.CheckLocalVariable("numberAndString", true);
            script.CheckLocalVariable("numberAndBool", true);
            script.CheckLocalVariable("numberAndList", true);
            script.CheckLocalVariable("numberAndTable", true);

            script.CheckLocalVariable("stringAndBool", true);
            script.CheckLocalVariable("stringAndList", true);
            script.CheckLocalVariable("stringAndTable", true);

            script.CheckLocalVariable("boolAndList", true);
            script.CheckLocalVariable("boolAndTable", true);

            script.CheckLocalVariable("listAndTable", true);

            script.CheckLocalVariable("nullReflexivity", false);
            script.CheckLocalVariable("nullAndValue", false);
            script.CheckLocalVariable("nullAndNonNullVariable", false);
        }
    }
}