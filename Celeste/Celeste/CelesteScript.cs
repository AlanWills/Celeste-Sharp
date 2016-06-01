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

        /// <summary>
        /// The scope created for all the scoped variables declared within this script
        /// </summary>
        internal Scope ScriptScope { get; private set; }

        #endregion

        public CelesteScript(string scriptPath)
        {
            ScriptPath = scriptPath;
            ScriptScope = new Scope(ScriptPath);
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
                
                // Reset the current scope to the global scope
                CelesteStack.Scopes.Remove(CelesteStack.CurrentScope);
                CelesteStack.CurrentScope = CelesteStack.GlobalScope;

                // Clear the stack - there should be no objects on the stack over different scripts
                CelesteStack.Clear();
            }
            else
            {
                Debug.Fail("Invalid filepath for script");
            }
        }

        #endregion
    }
}