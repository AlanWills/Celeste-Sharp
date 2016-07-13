using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteLogging : CelesteUnitTest
    {
        [TestMethod]
        public void TestCelesteLoggingTestWriting()
        {
            // Reset the log
            Cel.ClearLog();
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("test logging");
            }
        }

        [TestMethod]
        public void TestCelesteLoggingTestWritingAndReading()
        {
            // Reset the log
            Cel.ClearLog();
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("test logging");
                writer.WriteLine(true);
                writer.WriteLine(10);
            }

            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("test logging", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("10", reader.ReadLine());
            }
        }

        [TestMethod]
        public void TestCelesteLoggingTestOverwriting()
        {
            // Reset the log
            Cel.ClearLog();
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("test logging");
                writer.WriteLine(true);
                writer.WriteLine(10);
            }

            Cel.ClearLog();

            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual(null, reader.ReadLine());
            }
        }
    }
}
