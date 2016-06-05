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

        #endregion

        public CelesteScript(string scriptPath)
        {
            ScriptPath = scriptPath;
            ScriptScope = new Scope(ScriptPath);
        }

        #region Utility Functions

        /// <summary>
        /// Loads our file and sends it to the parser to compile into a statement tree
        /// </summary>
        public void Compile()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + ScriptPath))
            {
                Tuple<bool, CompiledStatement> result = CelesteCompiler.CompileScript(File.OpenText(Directory.GetCurrentDirectory() + "\\" + ScriptPath));

                if (result.Item1)
                {
                    // If we have successfully parsed the script, we execute the compile tree
                    ScriptExecutable = result.Item2;
                }
                else
                {
                    Debug.Fail("Script " + ScriptPath + " had an error during compilation");
                }
            }
            else
            {
                Debug.Fail("Invalid filepath for script");
            }
        }

        /// <summary>
        /// Calls the executable operation and runs the script
        /// </summary>
        public void Run()
        {
            Debug.Assert(ScriptExecutable != null, "Call Compile on this script before you run it");
            ScriptExecutable.PerformOperation();

            // Reset the current scope to the global scope
            CelesteStack.Scopes.Remove(CelesteStack.CurrentScope);
            CelesteStack.CurrentScope = CelesteStack.GlobalScope;

            // Clear the stack - there should be no objects on the stack over different scripts
            CelesteStack.Clear();
        }

        #endregion
    }
}