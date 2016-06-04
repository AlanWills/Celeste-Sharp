using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestOrKeyword : CelesteUnitTest
    {
        [TestMethod]
        public void TestOrKeywordOrNumbers()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Or\\TestOrKeywordOrNumbers.cel");

            script.CheckLocalVariable("intOr", false);
            script.CheckLocalVariable("intOr2", false);
            script.CheckLocalVariable("floatOr", false);
            script.CheckLocalVariable("floatOr2", false);
        }

        [TestMethod]
        public void TestOrKeywordOrStrings()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Or\\TestOrKeywordOrStrings.cel");

            script.CheckLocalVariable("stringOr", false);
            script.CheckLocalVariable("stringOr2", false);
            script.CheckLocalVariable("emptyOr", false);
            script.CheckLocalVariable("emptyOr2", false);
        }

        [TestMethod]
        public void TestOrKeywordOrBools()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Or\\TestOrKeywordOrBools.cel");

            script.CheckLocalVariable("trueOr", true);
            script.CheckLocalVariable("falseOr", false);
            script.CheckLocalVariable("trueFalseOr", true);
            script.CheckLocalVariable("notTrueFalseOr", false);
            script.CheckLocalVariable("trueNotFalseOr", true);
        }

        [TestMethod]
        public void TestOrKeywordOrReferences()
        {
            CelesteScript script = RunScript("TestScripts\\Keywords\\Or\\TestOrKeywordOrReferences.cel");

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
            script.CheckLocalVariable("notNullAndNonNullVariable", true);
        }
    }
}