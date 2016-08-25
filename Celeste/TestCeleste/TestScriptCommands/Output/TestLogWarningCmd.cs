using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestLogWarningCmd : CelesteUnitTest
    {
        [TestMethod]
        public void TestLogWarningCmdHardCodedValues()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Output\\LogWarningCmd\\TestLogWarningCmdHardCodedValues.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("logWarning"));

            string warningString = "Warning in script ScriptCommands\\Output\\LogWarningCmd\\TestLogWarningCmdHardCodedValues.cel";

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("hello", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("1", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("-1", reader.ReadLine());
            }
        }

        [TestMethod]
        public void TestLogWarningCmdVariables()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Output\\LogWarningCmd\\TestLogWarningCmdVariables.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("logWarning"));

            string warningString = "Warning in script ScriptCommands\\Output\\LogWarningCmd\\TestLogWarningCmdVariables.cel";

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("hello", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("10", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("-10", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("reference", reader.ReadLine());
            }
        }
    }
}