using System;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// Registers the API for outputting data that will be globally available in our scripts.
    /// Includes functions for printing and logging errors and warnings.
    /// </summary>
    [HasScriptCommands]
    internal class OutputScriptCommands
    {
        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the Console.
        /// Divert the Console output to print objects elsewhere.
        /// </summary>
        /// <param name="input"></param>
        [ScriptCommand("print")]
        public static void PrintCmd(CelesteObject input)
        {
            Console.WriteLine(input.ToString());
        }

        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the log file. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("log")]
        public static void LogCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message.ToString());
            }
        }

        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the log file with a warning message including the script name. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("logWarning")]
        public static void LogWarningCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Warning in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message.ToString());
            }
        }

        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the log file with an error message including the script name. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("logError")]
        public static void LogErrorCmd(CelesteObject message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Error in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message.ToString());
            }
        }

        /// <summary>
        /// Sets the file path of the log file.
        /// </summary>
        /// <param name="fullLogOutputFilePath"></param>
        [ScriptCommand("setLogFilePath")]
        public static void SetLogFilePath(CelesteObject fullLogOutputFilePath)
        {
            Cel.LogOutputFilePath = fullLogOutputFilePath.ToString();
        }

        /// <summary>
        /// Gets the current file path of the log file
        /// </summary>
        /// <returns></returns>
        [ScriptCommand("getLogFilePath")]
        public static string GetLogFilePath()
        {
            return Cel.LogOutputFilePath;
        }
    }
}