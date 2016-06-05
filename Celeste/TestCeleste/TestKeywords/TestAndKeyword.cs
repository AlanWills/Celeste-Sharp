using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestAndKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestAndKeywordAndNumbers()
        {
            CelesteScript script = RunScript("Keywords\\And\\TestAndKeywordAndNumbers.cel");

            script.CheckLocalVariable("intAnd", false);
            script.CheckLocalVariable("intAnd2", false);
            script.CheckLocalVariable("floatAnd", false);
            script.CheckLocalVariable("floatAnd2", false);
        }

        [TestMethod]
        public void TestAndKeywordAndStrings()
        {
            CelesteScript script = RunScript("Keywords\\And\\TestAndKeywordAndStrings.cel");

            script.CheckLocalVariable("stringAnd", false);
            script.CheckLocalVariable("stringAnd2", false);
            script.CheckLocalVariable("emptyAnd", false);
            script.CheckLocalVariable("emptyAnd2", false);
        }

        [TestMethod]
        public void TestAndKeywordAndBools()
        {
            CelesteScript script = RunScript("Keywords\\And\\TestAndKeywordAndBools.cel");

            script.CheckLocalVariable("trueAnd", true);
            script.CheckLocalVariable("falseAnd", false);
            script.CheckLocalVariable("trueFalseAnd", false);
            script.CheckLocalVariable("notFalseAndTrue", true);
            script.CheckLocalVariable("trueAndNotTrue", false);
        }

        [TestMethod]
        public void TestAndKeywordAndReferences()
        {
            CelesteScript script = RunScript("Keywords\\And\\TestAndKeywordAndReferences.cel");

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
            script.CheckLocalVariable("notNullAndNonNullVariable", true);
        }
    }
}