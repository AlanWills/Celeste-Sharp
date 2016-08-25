using System.Diagnostics;
using System.IO;
using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestSetLogFilePathCmd : CelesteUnitTest
    {
        [TestMethod]
        public void Test_SetLogFilePath_Cmd()
        {
            Debug.Fail("This will fail because new path is not relative it is hard coded.  Will need directory API for this (with namespacing)");
            RunScript("ScriptCommands\\Output\\SetLogFilePathCmd\\TestSetLogFilePath.cel");

            string warningString = "Warning in script ScriptCommands\\Output\\SetLogFilePathCmd\\TestSetLogFilePath.cel";
            string errorString = "Error in script ScriptCommands\\Output\\SetLogFilePathCmd\\TestSetLogFilePath.cel";

            Assert.IsTrue(File.Exists(Cel.LogOutputFilePath));

            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("hello", reader.ReadLine());

                Assert.AreEqual(warningString, reader.ReadLine());
                Assert.AreEqual("warning", reader.ReadLine());

                Assert.AreEqual(errorString, reader.ReadLine());
                Assert.AreEqual("error", reader.ReadLine());
            }
        }

    }
}