using System;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// Registers the core API that will be globally available in our scripts
    /// </summary>
    [HasScriptCommands]
    internal class CoreScriptCommands
    {
        [ScriptCommand("print")]
        public static void PrintCmd(object input)
        {
            Console.WriteLine(input.ToString());
        }

        [ScriptCommand("log")]
        public static void LogCmd(object message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message.ToString());
            }
        }

        [ScriptCommand("logWarning")]
        public static void LogWarningCmd(object message)
        {
            // Customise this later
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message.ToString());
            }
        }

        [ScriptCommand("logError")]
        public static void LogErrorCmd(object message)
        {
            // Customise this later
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message.ToString());
            }
        }

        // Commands for setting log output
    }
}