using System;

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

        // Script command for logging
        // Script command for debugging?
        // For debug info we will just need to add debug logging whenever we have an assert
        // Debug to the same place as logging?
    }
}