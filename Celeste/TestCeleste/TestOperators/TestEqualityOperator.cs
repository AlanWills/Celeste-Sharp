﻿using Celeste;
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

        [TestMethod]
        public void TestEqualityOperatorEquateBools()
        {
            CelesteScript script = RunScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorEquateBools.cel");

            script.CheckLocalVariable("trueEquality", true);
            script.CheckLocalVariable("falseEquality", true);
            script.CheckLocalVariable("boolInequality", false);
        }

        [TestMethod]
        public void TestEqualityOperatorEquateReferences()
        {
            CelesteScript script = RunScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorEquateReferences.cel");

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