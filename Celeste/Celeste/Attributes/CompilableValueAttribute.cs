using System;

namespace Celeste
{
    /// <summary>
    /// A simple attribute used to mark internal Value structures as being compilable.
    /// When we parse a token our Compiler will then check to see whether the token satsifies the requirement to be this Value.
    /// </summary>
    public class CompilableValue : Attribute
    {
        
    }
}
