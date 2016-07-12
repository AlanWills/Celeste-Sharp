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

        // Script command for changing Console output and debug information output (when we have added it)
        // For debug info we will just need to add logging whenever we have an assert
    }
}
