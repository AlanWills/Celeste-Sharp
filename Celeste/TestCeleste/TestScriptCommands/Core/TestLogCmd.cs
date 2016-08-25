using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestLogCmd : CelesteUnitTest
    {
        [TestMethod]
        public void TestLogCmdHardCodedValues()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Core\\LogCmd\\TestLogCmdHardCodedValues.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("log"));

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("hello", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("1", reader.ReadLine());
                Assert.AreEqual("-1", reader.ReadLine());
            }
        }

        [TestMethod]
        public void TestLogCmdVariables()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Core\\LogCmd\\TestLogCmdVariables.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("log"));

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("hello", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("10", reader.ReadLine());
                Assert.AreEqual("-10", reader.ReadLine());
                Assert.AreEqual("reference", reader.ReadLine());
            }
        }
    }
}