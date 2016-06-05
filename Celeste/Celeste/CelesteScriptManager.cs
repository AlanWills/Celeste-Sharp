using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// The class with which we manage and create our scripts
    /// </summary>
    public static class CelesteScriptManager
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

        private static string scriptDirectoryPath = @"C:\Users\Alan\Documents\Visual Studio 2015\Projects\CelesteGit\Celeste\TestCeleste\TestScripts";
        public static string ScriptDirectoryPath
        {
            get { return scriptDirectoryPath; }
            set { scriptDirectoryPath = value; }
        }

        private static Dictionary<string, CelesteScript> CompiledScripts = new Dictionary<string, CelesteScript>();

        #endregion

        private static void RecompileScript(object sender, FileSystemEventArgs args)
        {
            Debug.Assert(CompiledScripts.ContainsKey(args.Name));

            CelesteScript script = CompiledScripts[args.Name];

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

            if (File.Exists(scriptDirectoryPath + "\\" + scriptPath))
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
    }
}
