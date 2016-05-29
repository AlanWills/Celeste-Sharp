using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteScript
    {
        [TestMethod]
        public void TestCelesteScriptDotCelFileOpen()
        {
            string filePath = "TestScripts\\Types\\Number\\TestNumberParsing.cel";
            string dir = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            Assert.IsTrue(File.Exists(fullPath));

            FileStream reader = File.Open(fullPath, FileMode.Open);
            Assert.IsNotNull(reader);

            reader.Close();
        }
    }
}
