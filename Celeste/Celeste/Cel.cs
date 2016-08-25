using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// The class with which we manage various global variables and create our scripts
    /// </summary>
    public static class Cel
    {
        #region Properties and Fields

        private static string scriptDirectoryPath = Directory.GetCurrentDirectory() + @"\Scripts";
        public static string ScriptDirectoryPath
        {
            get { return scriptDirectoryPath; }
            set { scriptDirectoryPath = value; }
        }

        /// <summary>
        /// Creates a stream writer for the LogOutputFilePath.
        /// Do not call this unless you specify LogOutputFilePath.
        /// Will append to the file.
        /// Ownership of the LogWriter passes to whoever uses it.
        /// </summary>
        public static StreamWriter LogWriter { get { return new StreamWriter(LogOutputFilePath, true); } }

        /// <summary>
        /// Creates a stream reader for the LogOutputFilePath.
        /// Do not call this unless you specify LogOutputFilePath.
        /// Ownership of the LogReader passes to whoever uses it.
        /// </summary>
        public static StreamReader LogReader { get { return new StreamReader(LogOutputFilePath); } }

        /// <summary>
        /// Specify this filepath to overwrite the output of the log to a file.
        /// Will create a file if it does not exist.
        /// Default value is Cel.ScriptDirectoryPath + "\\Log.txt";
        /// </summary>
        public static string LogOutputFilePath { get; set; }

        private static Dictionary<string, CelesteScript> CompiledScripts = new Dictionary<string, CelesteScript>();

        #endregion

        /// <summary>
        /// Compiles and runs a script.
        /// </summary>
        /// <param name="relativeScriptPath">The relative path of the script from the scripts directory e.g. 'SubDirectory\\Script.cel'</param>
        /// <returns></returns>
        public static CelesteScript CreateScript(string relativeScriptPath)
        {
            CelesteScript script = null;
            if (CompiledScripts.TryGetValue(relativeScriptPath, out script))
            {
                return script;
            }

            if (File.Exists(Path.Combine(scriptDirectoryPath, relativeScriptPath)))
            {
                script = new CelesteScript(relativeScriptPath);
            }
            else
            {
                Debug.Fail("Invalid filepath for script");
            }

            CompiledScripts.Add(relativeScriptPath, script);
            return script;
        }

        /// <summary>
        /// Wipes the log file
        /// </summary>
        public static void ClearLog()
        {
            if (LogOutputFilePath != null && File.Exists(LogOutputFilePath))
            {
                File.WriteAllText(LogOutputFilePath, string.Empty);
            }
        }
    }
}
