using System;

namespace Celeste
{
    /// <summary>
    /// A simple attribute used to mark up classes for our Compiler to search them for script commands.
    /// If this attribute is not added, the functions marked with the ScriptCommand attribute will not be added at runtime.
    /// </summary>
    public class HasScriptCommands : Attribute
    {
        
    }
}
