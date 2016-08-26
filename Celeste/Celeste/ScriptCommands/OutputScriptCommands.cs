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
        /// Writes the inputted string to the console.
        /// Divert the Console output to print objects elsewhere.
        /// </summary>
        /// <param name="input"></param>
        [ScriptCommand("print")]
        public static void PrintCmd(string input)
        {
            Console.WriteLine(input);
        }

        /// <summary>
        ///Write's out the inputted string to the log file. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("log")]
        public static void LogCmd(string message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine(message);
            }
        }

        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the log file with a warning message including the script name. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("logWarning")]
        public static void LogWarningCmd(string message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Warning in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message);
            }
        }

        /// <summary>
        /// Calls ToString on the inputted object and write's it out to the log file with an error message including the script name. 
        /// </summary>
        /// <param name="message"></param>
        [ScriptCommand("logError")]
        public static void LogErrorCmd(string message)
        {
            using (StreamWriter writer = Cel.LogWriter)
            {
                writer.WriteLine("Error in script " + CelesteScript.CurrentRunningScript);
                writer.WriteLine(message);
            }
        }

        /// <summary>
        /// Sets the file path of the log file.
        /// </summary>
        /// <param name="fullLogOutputFilePath"></param>
        [ScriptCommand("setLogFilePath")]
        public static void SetLogFilePath(string fullLogOutputFilePath)
        {
            Cel.LogOutputFilePath = fullLogOutputFilePath;
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