using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteLogging
    {
        [TestMethod]
        public void TestCelesteLoggingTestWriting()
        {
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("test logging");
            }
        }

        [TestMethod]
        public void TestCelesteLoggingTestWritingAndReading()
        {
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
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("test logging");
                writer.WriteLine(true);
                writer.WriteLine(10);
            }

            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";

            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual(null, reader.ReadLine());
            }
        }
    }
}
