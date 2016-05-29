using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents a compiled list 
    /// </summary>
    internal class List : Value
    {
        #region Properties and Fields

        private List<object> ListRef { get; set; }

        private static string endDelimiter = "]";

        #endregion

        public List() :
            base(new List<object>())
        {
            ListRef = _Value as List<object>;
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // We keep parsing until we find a closing character for our list
            bool foundClosing = false;
            while (!foundClosing)
            {
                Debug.Assert(tokens.Count > 0 || lines.Count > 0, "List requires a closing ']'");

                // Create a new set of tokens if we have run out for this line
                if (tokens.Count == 0)
                {
                    tokens = new LinkedList<string>(lines.First.Value.Split(' '));
                    lines.RemoveFirst();
                }

                string nextToken = CelesteCompiler.PopToken();
                if (nextToken == endDelimiter)
                {
                    foundClosing = true;
                }
                else if (CelesteCompiler.CompileToken(nextToken))
                {
                    // Take the value that has been created from compiling this token and add it to our list
                    // Then remove the compiled statement - we do not want objects in our list pushed onto the stack
                    CompiledStatement valueCreated = parent.ChildCompiledStatements[parent.ChildCount - 1];
                    parent.RemoveAt(parent.ChildCount - 1);

                    // The value we created MUST be a value
                    Debug.Assert(valueCreated is Value);
                    ListRef.Add((valueCreated as Value)._Value);
                }
                else
                {
                    // Error message if we cannot parse the next token
                    Debug.Fail("Operator invalid on token: " + token);
                }
            }
        }

        #endregion
    }
}