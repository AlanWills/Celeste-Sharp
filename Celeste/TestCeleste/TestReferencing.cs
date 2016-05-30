using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestReferencing : CelesteUnitTest
    {
        [TestMethod]
        public void TestReferencingCelesteObjectAffectsNumberVariable()
        {
            Reference obj = new Reference(5.0f);
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual(5.0f, celesteObject.Value);

            obj.Value = 10.0f;
            Assert.AreEqual(10.0f, celesteObject.Value);
        }

        [TestMethod]
        public void TestReferencingCelesteObjectAffectsStringVariable()
        {
            Reference obj = new Reference("Test");
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual("Test", celesteObject.Value);

            obj.Value = "Test Change";
            Assert.AreEqual("Test Change", celesteObject.Value);
        }

        [TestMethod]
        public void TestReferencingScopedVariables()
        {
            CelesteScript script = new CelesteScript("TestScripts\\TestReferencingScopedVariables.cel");
            script.Run();

            CelesteTestUtils.CheckLocalVariable(script, "firstVariable", 10.0f);
            CelesteTestUtils.CheckLocalVariable(script, "secondVariable", 10.0f);

            CelesteTestUtils.CheckLocalVariableList(script, "firstList", new List<object>() { 10.0f });
            CelesteTestUtils.CheckLocalVariableList(script, "secondList", new List<object>() { 10.0f });
        }
    }
}
