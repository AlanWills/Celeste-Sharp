using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Celeste
{
    /// <summary>
    /// A class which loads in a script and runs it.
    /// </summary>
    public class CelesteScript
    {
        #region Properties and Fields

        /// <summary>
        /// The relative path to our script file from the root dir.
        /// </summary>
        private string ScriptPath { get; set; }

        #endregion

        public CelesteScript(string scriptPath)
        {
            ScriptPath = scriptPath;
        }

        #region Utility Functions

        /// <summary>
        /// Loads our file and sends it to the parser
        /// </summary>
        public void Run()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ScriptPath))
            {
                CompiledStatement rootStatement = CelesteCompiler.CompileScript(File.OpenText(Directory.GetCurrentDirectory() + "\\" + ScriptPath));
                rootStatement.PerformOperation();
            }
            else
            {
                Debug.Fail("Invalid filepath for script");
            }
        }

        #endregion
    }
}