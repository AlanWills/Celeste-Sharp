using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestGetLogFilePathCmd : CelesteUnitTest
    {
        [TestMethod]
        public void Test_GetLogFilePath_Cmd()
        {
            Cel.LogOutputFilePath = Cel.ScriptDirectoryPath + "\\Log.txt";
            CelesteScript script = RunScript("ScriptCommands\\Output\\GetLogFilePathCmd\\TestGetLogFilePath.cel");

            script.CheckLocalVariable("logPath", Cel.ScriptDirectoryPath + "\\Log.txt");
        }
    }
}