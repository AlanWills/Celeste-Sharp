using System;
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

        /// <summary>
        /// The compiled script which we can call PerformOperation() on to run the whole script
        /// </summary>
        private CompiledStatement ScriptExecutable { get; set; }

        /// <summary>
        /// This flag marks that we have begun compiling so should not run
        /// </summary>
        public bool Compiling { get; set; }

        /// <summary>
        /// This flag marks that we have begun running so should not call compile until it is finished
        /// </summary>
        public bool Running { get; set; }

        #endregion

        internal CelesteScript(string scriptPath)
        {
            ScriptPath = scriptPath;
        }

        #region Utility Functions

        /// <summary>
        /// Loads our file and sends it to the parser to compile into a statement tree
        /// </summary>
        public void Compile()
        {
            // Wait until we finish running
            while (Running || Compiling) { }

            Compiling = true;

            if (CelesteStack.Scopes.Exists(x => x == ScriptScope))
            {
                CelesteStack.Scopes.Remove(ScriptScope);
            }

            ScriptScope = new Scope(ScriptPath);

            Tuple<bool, CompiledStatement> result = null;
            using (StreamReader scriptReader = File.OpenText(CelesteScriptManager.ScriptDirectoryPath + "\\" + ScriptPath))
            {
                result = CelesteCompiler.CompileScript(scriptReader);
            }

            Debug.Assert(result != null);
            if (result.Item1)
            {
                // If we have successfully parsed the script, we execute the compile tree
                ScriptExecutable = result.Item2;
            }
            else
            {
                Debug.Fail("Script " + ScriptPath + " had an error during compilation");
            }

            Compiling = false;
        }

        /// <summary>
        /// Calls the executable operation and runs the script
        /// </summary>
        public void Run()
        {
            // Wait until we finish compiling before running
            while (Compiling) { }

            Running = true;

            Debug.Assert(ScriptExecutable != null, "Call Compile on this script before you run it");
            ScriptExecutable.PerformOperation();

            // Reset the current scope to the global scope
            CelesteStack.Scopes.Remove(ScriptScope);
            CelesteStack.CurrentScope = CelesteStack.GlobalScope;

            // Clear the stack - there should be no objects on the stack over different scripts
            CelesteStack.Clear();

            Running = false;
        }

        #endregion
    }
}