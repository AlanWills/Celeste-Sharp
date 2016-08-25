using System;
using System.Diagnostics;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// Registers the core API that will be globally available in our scripts
    /// </summary>
    [HasScriptCommands]
    internal class OutputScriptCommands
    {
        [ScriptCommand("print")]
        public static void PrintCmd(CelesteObject input)
        {
            Console.WriteLine(input.ToString());
        }

        [ScriptCommand("log")]
        public static void LogCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message.ToString());
            }
        }

        [ScriptCommand("logWarning")]
        public static void LogWarningCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Warning in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message.ToString());
            }
        }

        [ScriptCommand("logError")]
        public static void LogErrorCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Error in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message.ToString());
            }
        }

        [ScriptCommand("setLogFile")]
        public static void SetLogFile(CelesteObject fullLogOutputFilePath)
        {
            
        }
    }
}