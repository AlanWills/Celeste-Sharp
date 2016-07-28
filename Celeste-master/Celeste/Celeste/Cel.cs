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

        /// <summary>
        /// Reloads the script if changed
        /// </summary>
        private static FileSystemWatcher Watcher { get; set; }

        private static bool reRunScriptsWhenChanged = true;
        public static bool ReRunScriptsWhenChanged
        {
            set { reRunScriptsWhenChanged = value; }
        }

        private static bool recompileScriptsWhenChanged = false;
        public static bool RecompileScriptsWhenChanged
        {
            set
            {
                recompileScriptsWhenChanged = value;
                if (recompileScriptsWhenChanged && Watcher == null)
                {
                    Watcher = new FileSystemWatcher(ScriptDirectoryPath, "*.cel");
                    Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    Watcher.Changed += new FileSystemEventHandler(RecompileScript);
                    Watcher.IncludeSubdirectories = true;
                }

                Watcher.EnableRaisingEvents = recompileScriptsWhenChanged;
            }
        }
        
        private static string scriptDirectoryPath = Directory.GetCurrentDirectory() + @"\TestScripts";
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

        private static void RecompileScript(object sender, FileSystemEventArgs args)
        {
            Debug.Assert(CompiledScripts.ContainsKey(args.Name));

            CelesteScript script = CompiledScripts[args.Name];

            // Try to avoid spurious re-compiles and re-runs for multiple events sent within a short time of each other
            if (!script.Compiling && !script.Running)
            {
                script.Compile();

                if (reRunScriptsWhenChanged)
                {
                    script.Run();
                }
            }
        }

        public static CelesteScript CreateScript(string scriptPath)
        {
            CelesteScript script = null;
            if (CompiledScripts.TryGetValue(scriptPath, out script))
            {
                return script;
            }

            if (File.Exists(Path.Combine(scriptDirectoryPath, scriptPath)))
            {
                script = new CelesteScript(scriptPath);
            }
            else
            {
                Debug.Fail("Invalid filepath for script");
            }

            CompiledScripts.Add(scriptPath, script);
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
