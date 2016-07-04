using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    // We have to treat the testing of functions slightly differently, which is why they are in a separate test file
    [TestClass]
    public class TestCelesteObjectAsFunction : CelesteUnitTest
    {
        [TestMethod]
        public void TestCelesteObjectIsFunction()
        {
            CelesteScript script = RunScript("TestFunction.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunc"));
            Variable funcVar = script.ScriptScope.GetLocalVariable("testFunc");
            CelesteObject funcAsCelObject = new CelesteObject(funcVar);

            Assert.IsTrue(funcAsCelObject.IsFunction());
        }

        [TestMethod]
        public void TestCelesteObjectInvoke()
        {
            CelesteScript script = RunScript("TestFunction.cel");

            Assert.IsTrue(script.ScriptScope.VariableExists("testFunc"));
            CelesteObject funcAsCelObject = new CelesteObject(script.ScriptScope.GetLocalVariable("testFunc"));

            Assert.IsTrue(funcAsCelObject.IsFunction());

            CelesteObject parameter = CelesteObject.CreateVariable("");
            funcAsCelObject.Invoke(parameter);

            Assert.AreEqual("TestFunction", parameter.AsReference().Value);
        }

        // More tests
    }
}
